using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerStatePause : ITimerState
{
    public override void StateUpdate(float deltaTime)
    {
        timerModel.TotalTimeToUnitTime();
    }

    public override void OnClickPlayButton()
    {
        timerModel.timerState.Value = TimerModel.statePlaying;
    }

    public override void OnClickResetButton()
    {        
        timerModel.currentTime.Value = timerModel.originalTime;
        timerModel.TotalTimeToUnitTime();
        timerModel.timerState.Value = TimerModel.stateIdle;
    }
}
