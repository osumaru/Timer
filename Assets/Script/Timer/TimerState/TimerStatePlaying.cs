using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerStatePlaying : ITimerState
{
    public override void StateUpdate(float deltaTime)
    {
        timerModel.currentTime.Value -= deltaTime;
        timerModel.TotalTimeToUnitTime();
        if (timerModel.currentTime.Value <= 0.0f)
        {
            timerModel.currentTime.Value = 0.0f;
            timerModel.timerState.Value = TimerModel.stateStop;
        }
    }
    
    public override void OnClickPlayButton()
    {
        timerModel.timerState.Value = TimerModel.statePause;
    }

}
