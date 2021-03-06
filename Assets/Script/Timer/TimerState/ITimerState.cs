using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class ITimerState
{
    public TimerModel timerModel = null;
    public UnityEvent OnStateEnter { get; } = new UnityEvent();
    public UnityEvent OnStateExit { get; }  = new UnityEvent();
    
    public virtual void Enter()
    {
        OnStateEnter?.Invoke();
    }

    public virtual void Exit()
    {
        OnStateExit?.Invoke();
    }
    public virtual void StateUpdate(float deltaTime){}

    public virtual ITimerState OnPlayCommand(){ return null; }

    public virtual ITimerState OnResetCommand() { return null; }
    
    public virtual ITimerState OnStopCommand() { return null; }
    
    public virtual ITimerState OnPauseCommand() { return null; }
}
