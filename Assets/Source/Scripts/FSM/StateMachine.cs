using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine 
{
    public EntityState CurrentState { get; private set; }

    public void Init(EntityState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(EntityState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
        CurrentState.Enter();
    }
}
