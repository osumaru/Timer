using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class TimerPresenter : MonoBehaviour
{
    [SerializeField]
    TimerView view;

    TimerModel model;

    [SerializeField]
    FluctuationsNumPresenter hourNumPresenter;

    [SerializeField] 
    FluctuationsNumPresenter minutePresenter;

    [SerializeField]
    FluctuationsNumPresenter secondPresenter;


    
    // Start is called before the first frame update
    void Awake()
    {
        model = new TimerModel();
        model.Initialize(hourNumPresenter, minutePresenter, secondPresenter);
        view.OnClickPlay = model.OnClickPlayButton;
        view.OnClickReset = model.OnClickResetButton;
        model.timerState.Subscribe(value =>
        {
            view.OnStateChange(model.preTimerState, value);
            model.preTimerState = value;
        });
        model.currentTime.Subscribe(value =>
        {
            view.OnTimeChange(value, model.originalTime);
        });

        TimerModel.stateIdle.OnStateEnter.AddListener(view.StateIdleEnter);
        TimerModel.statePause.OnStateEnter.AddListener(view.StatePauseEnter);
        TimerModel.stateStop.OnStateEnter.AddListener(view.StateStopEnter);
        TimerModel.statePlaying.OnStateEnter.AddListener(view.StatePlayingEnter);
        TimerModel.statePause.OnStateExit.AddListener(view.StatePauseExit);
        TimerModel.stateStop.OnStateExit.AddListener(view.StateStopExit);
        FluctuationsNumPresenter[] presenters =
        {
            hourNumPresenter,
            minutePresenter,
            secondPresenter
        };
        foreach (FluctuationsNumPresenter presenter in presenters)
        {
            TimerModel.stateIdle.OnStateEnter.AddListener(presenter.view.OnButtonActive);
            TimerModel.stateIdle.OnStateExit.AddListener(presenter.view.OnButtonDeactive);
        }
        view.OnTimeUpdate = model.TimeUpdate;
        model.timerState.Value = TimerModel.stateIdle;
    }
}
