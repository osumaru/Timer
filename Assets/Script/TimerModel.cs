using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using System;

public class TimerModel
{
    public enum State
    {
        Idle,
        Playing,
        Pause,
        Stop
    }

    FluctuationsNumPresenter hourNumPresenter;
    FluctuationsNumPresenter minuteNumPresenter;
    FluctuationsNumPresenter secondNumPresenter;


    int hourTime = 0;
    int minuteTime = 0;
    int secondTime = 0;



    private State timerState = State.Idle;

    public State TimerState
    {
        get { return timerState; }
        set
        {
            if (timerState != value)
            {
                timerState = value;
                OnStateChange(timerState);
            }
        }
    }
    float currentTime = 0;

    public float CurrentTime
    {
        get { return currentTime;}
        set
        {
            currentTime = value;
            OnTimeChange(currentTime, originalTime);
        }
    }
    
    float originalTime = 0.0f;

    public Action<State> OnStateChange;
    public Action<float, float> OnTimeChange;
    public void Initialize(
        FluctuationsNumPresenter hourPresenter,
        FluctuationsNumPresenter minutePresenter,
        FluctuationsNumPresenter secondPresenter)
    {
        hourNumPresenter = hourPresenter;
        minuteNumPresenter = minutePresenter;
        secondNumPresenter = secondPresenter;
    }

    // Update is called once per frame
    public void TimeUpdate(float deltaTime)
    {
        
        switch (TimerState)
        {
            case State.Idle:
                InputNum();
                CurrentTime = originalTime;
                break;
            case State.Stop:
                break;
            case State.Playing:
                CurrentTime -= deltaTime;
                ShowNum();
                if (CurrentTime <= 0.0f)
                {
                    CurrentTime = 0.0f;
                    //if (sound == null)
                    {
                        hourNumPresenter.Model.CurrentNum = 0;
                        hourNumPresenter.Model.CurrentNum = 0;
                        hourNumPresenter.Model.CurrentNum = 0;
                        TimerState = State.Stop;
                    }
                }

                break;
            case State.Pause:
                ShowNum();
                break;
        }
    }

    void ShowNum()
    {
        float showTime = CurrentTime + 1.0f - 1E-06f;
        int intHourNum = (int) Mathf.Floor(showTime / 3600.0f);
        hourNumPresenter.Model.CurrentNum = intHourNum;
        showTime -= 3600.0f * intHourNum;
        int intMinuteNum = (int) Mathf.Floor(showTime / 60.0f);
        minuteNumPresenter.Model.CurrentNum = intMinuteNum;
        showTime -=  60.0f * intMinuteNum;
        int intSecondNum = (int) Mathf.Floor(showTime);
        secondNumPresenter.Model.CurrentNum = intSecondNum;
    }

    void InputNum()
    {
        float hourTime = hourNumPresenter.Model.CurrentNum * 3600.0f;
        float minuteTime = minuteNumPresenter.Model.CurrentNum * 60.0f;
        float secondTime = secondNumPresenter.Model.CurrentNum;
        originalTime = hourTime + minuteTime + secondTime;
    }

    public void OnClickPlayButton()
    {
        switch (TimerState)
        {
            case State.Idle:
                TimerState = State.Playing;
                break;
            case State.Pause:
                TimerState = State.Playing;
                break;
            case State.Playing:
                TimerState= State.Pause;
                break;
            case State.Stop:
                CurrentTime = originalTime;
                ShowNum();
                TimerState = State.Idle;
                break;
        }
    }

    public void OnClickResetButton()
    {
        switch (TimerState)
        {
            case State.Idle:
                break;
            case State.Pause:
                CurrentTime = originalTime;
                ShowNum();
                TimerState = State.Idle;
                break;
            case State.Playing:
                break;
            case State.Stop:
                CurrentTime = originalTime;
                ShowNum();
                TimerState = State.Idle;
                break;
        }
    }
}