using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerStateStop : ITimerState
{

    public override ITimerState OnClickPlayButton()
    {
        return TimerModel.stateIdle;
    }

    public override ITimerState OnClickResetButton()
    {
        return = TimerModel.stateIdle;
    }
}
