using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerStateStop : ITimerState
{
    public override void StateUpdate(float deltaTime)
    {
    }

    public override void OnClickPlayButton()
    {
        timerModel.currentTime.Value = timerModel.originalTime;
        timerModel.TotalTimeToUnitTime();
        timerModel.timerState.Value = TimerModel.stateIdle;
    }

    public override void OnClickResetButton()
    {
        timerModel.currentTime.Value = timerModel.originalTime;
        timerModel.TotalTimeToUnitTime();
        timerModel.timerState.Value = TimerModel.stateIdle;
    }
}
