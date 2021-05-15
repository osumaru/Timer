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
        
        KeyCode[] keyCodes = new KeyCode[]
        {
            KeyCode.Keypad0,
            KeyCode.Keypad1,
            KeyCode.Keypad2,
            KeyCode.Keypad3,
            KeyCode.Keypad4,
            KeyCode.Keypad5,
            KeyCode.Keypad6,
            KeyCode.Keypad7,
            KeyCode.Keypad8,
            KeyCode.Keypad9
        };
        int idx = 0;
        
        foreach (KeyCode keycode in keyCodes)
        {
            if (Input.GetKeyDown(keycode))
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
    
    
}