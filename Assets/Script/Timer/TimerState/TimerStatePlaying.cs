using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerStatePlaying : ITimerState
{
    public override void StateUpdate(float deltaTime)
    {
        timerModel.currentTime.Value -= deltaTime;
        if (timerModel.currentTime.Value <= 0.0f)
        {
            timerModel.currentTime.Value = 0.0f;
            timerModel.timerState.Value = timerModel.StateAlarm;
        }
    }
    
    public override ITimerState OnPauseCommand()
    {
        return timerModel.statePause;
    }

}
