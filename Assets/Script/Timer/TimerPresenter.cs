using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;


public class TimerPresenter : MonoBehaviour
{

    TimerModel model;

    public TimerModel Model
    {
        get => model;
    }
        
    [SerializeField]
    TimerView view;

    [SerializeField]
    ProgressBarView progressBarView;

    [SerializeField]
    TimerStateView stateView;

    [SerializeField]
    FluctuationsNumView[] numViews;
    
    // Start is called before the first frame update
    void Awake()
    {
        model = new TimerModel();
        model.Initialize();
        stateView.OnClickPlay = model.OnClickPlayButton;
        stateView.OnClickReset = model.OnClickResetButton;
        stateView.OnClickStop = model.OnClickStopButton;
        stateView.OnClickPause = model.OnClickPauseButton;
        model.timerState.Subscribe(nextState =>
        {
            model.preTimerState?.Exit();
            nextState?.Enter();
            model.preTimerState = nextState;
        });
        model.currentTime.Subscribe(value =>
        {
            progressBarView?.OnTimeChange(value, model.originalTime);
            foreach (var numView in numViews)
            {
                numView.OnTimeChange(value);
                
            }
        });

        model.stateIdle.OnStateEnter.AddListener(stateView.StateIdleEnter);
        model.statePause.OnStateEnter.AddListener(stateView.StatePauseEnter);
        model.StateAlarm.OnStateEnter.AddListener(stateView.StateAlarmEnter);
        model.statePlaying.OnStateEnter.AddListener(stateView.StatePlayingEnter);
        model.statePause.OnStateExit.AddListener(stateView.StatePauseExit);
        model.StateAlarm.OnStateExit.AddListener(stateView.StateAlarmExit);
        foreach (var numView in numViews)
        {
            numView.OnAddNum = model.AddNum;
            model.stateIdle.OnStateEnter.AddListener(numView.OnButtonActive);
            model.stateIdle.OnStateExit.AddListener(numView.OnButtonDeactive);
            numView.Initialized();
        }
        view.OnTimeUpdate = model.TimeUpdate;
        model.timerState.Value = model.stateIdle;
    }
}
