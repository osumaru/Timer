using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class TimerStateIdle : ITimerState
{
    public override void Enter()
    {
        base.Enter();
        timerModel.currentTime.Value = timerModel.originalTime;
    }

    public override void StateUpdate(float deltaTime)
    {
        timerModel.currentTime.Value = timerModel.originalTime;
    }

    public override ITimerState OnPlayCommand()
    {

        return timerModel.statePlaying;
    }
    
}
