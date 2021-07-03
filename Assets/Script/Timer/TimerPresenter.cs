using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;


public class TimerPresenter : MonoBehaviour
{

    TimerModel model;

    [SerializeField]
    TimerView view;

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
            view.OnTimeChange(value, model.originalTime);
            foreach (var numView in numViews)
            {
                numView.OnTimeChange(value);
                
            }
        });

        TimerModel.stateIdle.OnStateEnter.AddListener(stateView.StateIdleEnter);
        TimerModel.statePause.OnStateEnter.AddListener(stateView.StatePauseEnter);
        TimerModel.StateAlarm.OnStateEnter.AddListener(stateView.StateAlarmEnter);
        TimerModel.statePlaying.OnStateEnter.AddListener(stateView.StatePlayingEnter);
        TimerModel.statePause.OnStateExit.AddListener(stateView.StatePauseExit);
        TimerModel.StateAlarm.OnStateExit.AddListener(stateView.StateAlarmExit);
        foreach (var numView in numViews)
        {
            numView.OnAddNum = model.AddNum;
            TimerModel.stateIdle.OnStateEnter.AddListener(numView.OnButtonActive);
            TimerModel.stateIdle.OnStateExit.AddListener(numView.OnButtonDeactive);
            numView.Initialized();
        }
        view.OnTimeUpdate = model.TimeUpdate;
        model.timerState.Value = TimerModel.stateIdle;
    }
}
