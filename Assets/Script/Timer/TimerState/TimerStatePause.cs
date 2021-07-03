using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerStatePause : ITimerState
{

    public override ITimerState OnPlayCommand()
    {
        return TimerModel.statePlaying;
    }

    public override ITimerState OnResetCommand()
    {        
        return TimerModel.stateIdle;
    }
    public override ITimerState OnStopCommand()
    {        
        return TimerModel.stateIdle;
    }
}
