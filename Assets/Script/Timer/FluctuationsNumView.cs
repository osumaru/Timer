using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using UniRx;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class FluctuationsNumView : MonoBehaviour
{
    [SerializeField]
    int maxNum = 100;

    [SerializeField]
    int unitScale = 0;

    [SerializeField] 
    Button countUpButton;

    [SerializeField] 
    Button countDonwButton;

    [SerializeField]
    Image activeImage;
    
    [SerializeField] 
    Text viewNum;

    [SerializeField]
    EventTrigger eventTrigger;

    public Action<int> OnAddNum;

    ReactiveProperty<bool> isButtonActive = new ReactiveProperty<bool>(false);

    bool isPointerStay = false;

    bool isActiveButton = true;

    int currentNum = 0;
    int previousNum = 0;
    struct KeyNumInput
    {
        public KeyCode keyPad;
        public KeyCode alpha;
    }
    
    
    readonly KeyNumInput[] keyCodes = 
    {
        new KeyNumInput{alpha = KeyCode.Alpha0,keyPad = KeyCode.Keypad0},
        new KeyNumInput{alpha = KeyCode.Alpha1,keyPad = KeyCode.Keypad1},
        new KeyNumInput{alpha = KeyCode.Alpha2,keyPad = KeyCode.Keypad2},
        new KeyNumInput{alpha = KeyCode.Alpha3,keyPad = KeyCode.Keypad3},
        new KeyNumInput{alpha = KeyCode.Alpha4,keyPad = KeyCode.Keypad4},
        new KeyNumInput{alpha = KeyCode.Alpha5,keyPad = KeyCode.Keypad5},
        new KeyNumInput{alpha = KeyCode.Alpha6,keyPad = KeyCode.Keypad6},
        new KeyNumInput{alpha = KeyCode.Alpha7,keyPad = KeyCode.Keypad7},
        new KeyNumInput{alpha = KeyCode.Alpha8,keyPad = KeyCode.Keypad8},
        new KeyNumInput{alpha = KeyCode.Alpha9,keyPad = KeyCode.Keypad9},
    };
    

    // Start is called before the first frame update
    void Awake()
    {
        countDonwButton.onClick.AddListener(OnClickCountDownButton);
        countUpButton.onClick.AddListener(OnClickCountUpButton);
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener(OnPointerEnter);
        eventTrigger.triggers.Add(entry);
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerExit;
        entry.callback.AddListener(OnPointerExit);
        eventTrigger.triggers.Add(entry);
        isButtonActive.Subscribe(OnButtonActiveImage);
    }

    public void Initialized()
    {
        OnChangedNum(0);
    }

    public void OnChangedNum(int num)
    {
         int showNum = num / unitScale;
        viewNum.text = showNum.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        int idx = 0;
        foreach (var keycode in keyCodes)
        {
            if (Input.GetKeyDown(keycode.alpha) || Input.GetKeyDown(keycode.keyPad))
            {
                OnInputNum(idx);
            }

            idx++;
        }
        if (Input.GetKeyDown(KeyCode.Delete) || Input.GetKeyDown(KeyCode.Backspace))
        {
            OnDeleteNum();
        }
        OnInputMouseWheel(Input.mouseScrollDelta.y);
        if(Input.GetMouseButtonDown(0))
        {
            OnClick();
        }
    }

    void OnClick()
    {
        isButtonActive.Value = isPointerStay;
    }
    void OnClickCountUpButton()
    {
        AddNumOnUnitScale(1);
    }

    void OnClickCountDownButton()
    {
        AddNumOnUnitScale(-1);
    }

    void OnPointerEnter(BaseEventData eventData)
    {
        isPointerStay = true;
    }

    void OnPointerExit(BaseEventData eventData)
    {
        isPointerStay = false;
    }

    public void OnButtonActiveImage(bool isButtonActive)
    {
        activeImage.gameObject.SetActive(isButtonActive && isActiveButton);
    }

    public void OnButtonDeactive()
    {
        isActiveButton = false;
        countDonwButton.gameObject.SetActive(false);
        countUpButton.gameObject.SetActive(false);
    }
    
    public void OnButtonActive()
    {
        isActiveButton = true;
        countDonwButton.gameObject.SetActive(true);
        countUpButton.gameObject.SetActive(true);
    }


    public void OnDeleteNum()
    {
        if (isButtonActive.Value)
        {
            AddNumOnUnitScale(0 - currentNum);
        }
    }


    public void OnInputMouseWheel(float inputWheel)
    {
        if (isPointerStay)
        {
            AddNumOnUnitScale((int)inputWheel);
        }
    }
    public void OnInputNum(int num)
    {
        if (isButtonActive.Value)
        {
            int tempNum = currentNum;
            tempNum *= 10;
            tempNum += num;
            tempNum %= 100;
            if (tempNum < maxNum)
            {
                AddNumOnUnitScale(tempNum - currentNum);
            }
        }
    }

    void AddNumOnUnitScale(int num)
    {
        int preNum = currentNum;
        currentNum += num * unitScale;
        while (currentNum < 0)
        {
            currentNum += maxNum * unitScale;
        }

        currentNum %= maxNum * unitScale;
        int addTime = currentNum - preNum;
        OnAddNum(addTime);
        OnChangedNum(currentNum);
    }

    public void OnTimeChange(float timerCurrentNum)
    {
        int newNum = (int) Mathf.Ceil(timerCurrentNum) / unitScale;
        newNum %= maxNum;
        currentNum = newNum * unitScale;
        OnChangedNum(currentNum);
    }
}