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
    FluctuationsNumView[] numViews;
    
    // Start is called before the first frame update
    void Awake()
    {
        model = new TimerModel();
        model.Initialize();
        view.OnClickPlay = model.OnClickPlayButton;
        view.OnClickReset = model.OnClickResetButton;
        model.timerState.Subscribe(nextState =>
        {
            model.preTimerState?.Exit();
            nextState?.Enter();
            model.preTimerState = nextState;
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
        foreach (var view in numViews)
        {
            view.OnAddNum = model.AddNum;
            TimerModel.stateIdle.OnStateEnter.AddListener(view.OnButtonActive);
            TimerModel.stateIdle.OnStateExit.AddListener(view.OnButtonDeactive);
        }
        view.OnTimeUpdate = model.TimeUpdate;
        model.timerState.Value = TimerModel.stateIdle;
    }
}
