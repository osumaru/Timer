using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;


public class FluctuationsNumView : MonoBehaviour
{
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

    public Action OnClickCountUp;

    public Action OnClickCountDown;

    public Action OnClickNum;

    public Action OnPointerExitFunc;

    public Action OnPointerEnterFunc;

    public Action<int> OnInputNum;

    public Action OnDeleteNum;

    public Action<float> OnInputWheel;

    bool isActiveButton = true;
    struct hogehoge
    {
        public KeyCode keyPad;
        public KeyCode alpha;
    }
    
    
    readonly hogehoge[] keyCodes = 
    {
        new hogehoge{alpha = KeyCode.Alpha0,keyPad = KeyCode.Keypad0},
        new hogehoge{alpha = KeyCode.Alpha1,keyPad = KeyCode.Keypad1},
        new hogehoge{alpha = KeyCode.Alpha2,keyPad = KeyCode.Keypad2},
        new hogehoge{alpha = KeyCode.Alpha3,keyPad = KeyCode.Keypad3},
        new hogehoge{alpha = KeyCode.Alpha4,keyPad = KeyCode.Keypad4},
        new hogehoge{alpha = KeyCode.Alpha5,keyPad = KeyCode.Keypad5},
        new hogehoge{alpha = KeyCode.Alpha6,keyPad = KeyCode.Keypad6},
        new hogehoge{alpha = KeyCode.Alpha7,keyPad = KeyCode.Keypad7},
        new hogehoge{alpha = KeyCode.Alpha8,keyPad = KeyCode.Keypad8},
        new hogehoge{alpha = KeyCode.Alpha9,keyPad = KeyCode.Keypad9}
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
        
    }

    // Update is called once per frame
    void Update()
    {
        int idx = 0;
        foreach (hogehoge keycode in keyCodes)
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
        OnInputWheel(Input.mouseScrollDelta.y);
        if(Input.GetMouseButtonDown(0) && OnClickNum != null)
        {
            OnClickNum();
        }
    }

    void OnClickCountUpButton()
    {
        if (OnClickCountUp != null)
        {
            OnClickCountUp();
        }
    }

    void OnClickCountDownButton()
    {
        if (OnClickCountDown != null)
        {
            OnClickCountDown();
        }
    }

    public void OnChangedNum(int num)
    {
        viewNum.text = num.ToString();
    }

    void OnPointerEnter(BaseEventData eventData)
    {
        if (OnPointerEnterFunc != null)
        {
            OnPointerEnterFunc();
        }
    }

    void OnPointerExit(BaseEventData eventData)
    {
        if (OnPointerExitFunc != null)
        {
            OnPointerExitFunc();
        }
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
}