using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerStateAlarm : ITimerState
{
    public override ITimerState OnStopCommand()
    {
        return timerModel.stateIdle;
    }
    
    public override ITimerState OnResetCommand()
    {
        return timerModel.stateIdle;
    }
}
