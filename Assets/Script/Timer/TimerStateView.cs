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
    Button resetButton;
    
    [SerializeField]
    Text playButtonText;
    
    [SerializeField]
    Image resetButtonImage;    
    
    [SerializeField]
    Color ActiveColor = Color.white;
    
    [SerializeField]
    Color DeactiveColor = Color.black;
    
    [SerializeField]
    GameObject alarmPrefab;
    
    GameObject alarm;
    
    public Action OnClickPlay;
    public Action OnClickReset;
    public Action OnClickStop;
    public Action OnClickPause;
    TimerState timerState = TimerState.Idle;

    enum TimerState
    {
        Idle,
        Playing,
        Alarm,
        Pause,
    }

    readonly string playText = "Play";
    readonly string stopText = "Stop";

    void Awake()
    {
        playButtonText.text = playText;
        playButton.onClick.AddListener(OnClickPlayButton);
        resetButton.onClick.AddListener(OnClickResetButton);
    }
    
    public void StateIdleEnter()
    {
        if (alarm != null)
        {
            Destroy(alarm);
        }
        playButtonText.text = playText;
        ResetButtonDeactive();
        timerState = TimerState.Idle;

    }

    public void StatePauseEnter()
    {
        playButtonText.text = playText;
        ResetButtonActive();
        timerState = TimerState.Pause;
    }

    public void StatePauseExit()
    {
        ResetButtonDeactive();
    }
    public void StatePlayingEnter()
    {
        playButtonText.text = stopText;
        timerState = TimerState.Playing;
    }

    public void StateAlarmEnter()
    {
        if (alarm == null)
        {
            alarm = Instantiate(alarmPrefab);
        }
        playButtonText.text = stopText;
        ResetButtonActive();
        timerState = TimerState.Alarm;
    }

    public void StateAlarmExit()
    {
        ResetButtonDeactive();
    }
    
    void ResetButtonDeactive()
    {
        resetButton.enabled = false;
        resetButtonImage.color = DeactiveColor;
    }

    void ResetButtonActive()
    {
        resetButton.enabled = true;
        resetButtonImage.color = ActiveColor;
    }
    
    public void OnClickPlayButton()
    {
        switch (timerState)
        {
            case TimerState.Idle:
                OnClickPlay?.Invoke();
                break;
            case TimerState.Alarm:
                OnClickStop?.Invoke();
                break;
            case TimerState.Pause:
                OnClickPlay?.Invoke();
                break;
            case TimerState.Playing:
                OnClickPause?.Invoke();
                break;
        }
    }
    public void OnClickResetButton()
    {        
        switch (timerState)
        {
            case TimerState.Alarm:
            case TimerState.Pause:
                OnClickReset?.Invoke();
                break;
        }
    }
}
