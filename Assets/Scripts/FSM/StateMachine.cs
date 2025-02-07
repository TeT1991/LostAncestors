using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

public class StateMachine
{
    private State _currentState;
    private Dictionary<States, State> _states;

    public int Count => _states.Count;

    public StateMachine(State InitState)
    {
        _states = new Dictionary<States, State>();  
        _currentState = InitState;
        _currentState.Enter();
    }

    public void ChangeState(State newState)
    {
        _currentState.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

    public void ChangState(States newStateType)
    {
        if (_states.TryGetValue(newStateType, out State tempState))
        {
            _currentState.Exit();
            _currentState = tempState;
            _currentState.Enter();
        }
    }

    public void AddState(States stateType, State state)
    {
        _states.Add(stateType, state);
    }

    public States GetCurrentStateType()
    {
        var key = _states.FirstOrDefault(x => x.Value == _currentState).Key;
        return key;
    }
}
