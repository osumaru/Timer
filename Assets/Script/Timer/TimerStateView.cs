using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using System;
using System.Data.Common;

public class TimerStateView : MonoBehaviour
{
    
    [SerializeField] 
    Button playButton;

    [SerializeField]
    Dropdown dropdown;
    
    [SerializeField]
    GameObject alarmPrefab;
    
    GameObject alarm;
    
    public Action OnClickPlay;
    public Action OnClickReset;
    public Action OnClickStop;
    public Action OnClickPause;
    TimerState timerState = TimerState.Stop;

    enum TimerState
    {
        Stop,
        Play,
        Pause,
        Reset,
        stateNum,
    }


    void Awake()
    {
        playButton.onClick.AddListener(OnClickPlayButton);
        List<Dropdown.OptionData> optionList = new List<Dropdown.OptionData>();
        for (int i = 0; i < (int) TimerState.stateNum; i++)
        {
            optionList.Add(new Dropdown.OptionData(((TimerState) i).ToString()));
        }
        dropdown.AddOptions(optionList);
    }
    
    public void StateIdleEnter()
    {
        if (alarm != null)
        {
            Destroy(alarm);
        }

    }

    public void StatePauseEnter()
    {
    }

    public void StatePauseExit()
    {
    }
    public void StatePlayingEnter()
    {
    }

    public void StateAlarmEnter()
    {
        if (alarm == null)
        {
            alarm = Instantiate(alarmPrefab);
        }
    }

    public void StateAlarmExit()
    {
    }

    public void OnClickPlayButton()
    {
        switch ((TimerState)dropdown.value)
        {
            case TimerState.Play:
                OnClickPlay?.Invoke();
                break;
            case TimerState.Stop:
                OnClickStop?.Invoke();
                break;
            case TimerState.Pause:
                OnClickPause?.Invoke();
                break;
            case TimerState.Reset:
                OnClickReset?.Invoke();
                break;
        }
    }
}
