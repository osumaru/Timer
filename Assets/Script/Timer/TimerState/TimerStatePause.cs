using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerStatePause : ITimerState
{

    public override ITimerState OnPlayCommand()
    {
        return timerModel.statePlaying;
    }

    public override ITimerState OnResetCommand()
    {        
        return timerModel.stateIdle;
    }
}
