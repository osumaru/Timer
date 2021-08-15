using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using System;
using UniRx;

public class TimerModel
{

    public TimerStateIdle stateIdle { get; }  = new TimerStateIdle();
    public TimerStatePause statePause { get; }  = new TimerStatePause();
    public TimerStateAlarm StateAlarm { get; }  = new TimerStateAlarm();
    public TimerStatePlaying statePlaying { get; }  = new TimerStatePlaying();

    public ReactiveProperty<ITimerState> timerState { get; } = new ReactiveProperty<ITimerState>();
    public ITimerState preTimerState;

    public ReactiveProperty<float> currentTime { get; } = new ReactiveProperty<float>(0.0f);

    public float originalTime = 0.0f;
    

    public void Initialize()
    {
        stateIdle.timerModel = this;
        statePause.timerModel = this;
        statePlaying.timerModel = this;
        StateAlarm.timerModel = this;
    }

    public void TimeUpdate(float deltaTime)
    {
        timerState.Value.StateUpdate(deltaTime);
    }

    public void AddNum(int addNum)
    {
        originalTime += addNum;
    }
   

    public void OnClickPlayButton()
    {
        ITimerState state = timerState.Value.OnPlayCommand();
        timerState.Value = state ?? timerState.Value;
    }

    public void OnClickResetButton()
    {
        ITimerState state = timerState.Value.OnResetCommand();
        timerState.Value = state ?? timerState.Value;
    }
    
    public void OnClickStopButton()
    {
        ITimerState state = timerState.Value.OnStopCommand();
        timerState.Value = state ?? timerState.Value;
    }
    
    public void OnClickPauseButton()
    {
        ITimerState state = timerState.Value.OnPauseCommand();
        timerState.Value = state ?? timerState.Value;
    }
}