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
    FluctuationsNumPresenter hourNumPresenter;
    FluctuationsNumPresenter minuteNumPresenter;
    FluctuationsNumPresenter secondNumPresenter;

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

    public void Initialize(
        FluctuationsNumPresenter hourPresenter,
        FluctuationsNumPresenter minutePresenter,
        FluctuationsNumPresenter secondPresenter)
    {
        hourNumPresenter = hourPresenter;
        minuteNumPresenter = minutePresenter;
        secondNumPresenter = secondPresenter;
        stateIdle.timerModel = this;
        statePause.timerModel = this;
        statePlaying.timerModel = this;
        stateStop.timerModel = this;
    }

    public void TimeUpdate(float deltaTime)
    {
        timerState.Value.StateUpdate(deltaTime);
    }

    
    /// <summary>
    /// 秒単位で持っている時間を単位毎に変換して振り分け
    /// </summary>
    public void TotalTimeToUnitTime()
    {
        float showTime = currentTime.Value + 1.0f - 1E-06f;
        int intHourNum = (int) Mathf.Floor(showTime / secondToHourTime);
        hourNumPresenter.Model.CurrentNum = intHourNum;
        showTime -= 3600.0f * intHourNum;
        int intMinuteNum = (int) Mathf.Floor(showTime / secondToMinuteTime);
        minuteNumPresenter.Model.CurrentNum = intMinuteNum;
        showTime -=  60.0f * intMinuteNum;
        int intSecondNum = (int) Mathf.Floor(showTime);
        secondNumPresenter.Model.CurrentNum = intSecondNum;
    }
    
    /// <summary>
    /// 単位毎で持っている時間を合計して秒単位に変換
    /// </summary>
    public void UnitTimeToTotalTime()
    {
        float hourTime = hourNumPresenter.Model.CurrentNum * secondToHourTime;
        float minuteTime = minuteNumPresenter.Model.CurrentNum * secondToMinuteTime;
        float secondTime = secondNumPresenter.Model.CurrentNum;
        originalTime = hourTime + minuteTime + secondTime;
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