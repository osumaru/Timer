using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class TimerStateIdle : ITimerState
{
    public override void StateUpdate(float deltaTime)
    {
        timerModel.UnitTimeToTotalTime();
        timerModel.currentTime.Value = timerModel.originalTime;
    }

    public override void OnClickPlayButton()
    {

        timerModel.timerState.Value = TimerModel.statePlaying;
    }

    public override void OnClickResetButton()
    {
    }
    
}
