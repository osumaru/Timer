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

    public static TimerStateIdle stateIdle { get; }  = new TimerStateIdle();
    public static TimerStatePause statePause { get; }  = new TimerStatePause();
    public static TimerStateStop stateStop { get; }  = new TimerStateStop();
    public static TimerStatePlaying statePlaying { get; }  = new TimerStatePlaying();

    public ReactiveProperty<ITimerState> timerState { get; } = new ReactiveProperty<ITimerState>();
    public ITimerState preTimerState;

    public ReactiveProperty<float> currentTime { get; } = new ReactiveProperty<float>(0.0f);

    public float originalTime = 0.0f;

    readonly float secondToHourTime = 3600.0f;
    readonly float secondToMinuteTime = 60.0f;

    public void Initialize()
    {
        stateIdle.timerModel = this;
        statePause.timerModel = this;
        statePlaying.timerModel = this;
        stateStop.timerModel = this;
    }

    public void TimeUpdate(float deltaTime)
    {
        timerState.Value.StateUpdate(deltaTime);
    }

    public void AddNum(int addNum)
    {
        currentTime.Value += addNum;
    }
   

    public void OnClickPlayButton()
    {
        timerState.Value.OnClickPlayButton();
    }

    public void OnClickResetButton()
    {
        timerState.Value.OnClickResetButton();
    }
}