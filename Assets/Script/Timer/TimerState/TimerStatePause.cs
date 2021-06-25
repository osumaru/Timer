using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerStatePause : ITimerState
{

    public override ITimerState OnClickPlayButton()
    {
        return TimerModel.statePlaying;
    }

    public override ITimerState OnClickResetButton()
    {        
        return TimerModel.stateIdle;
    }
}
