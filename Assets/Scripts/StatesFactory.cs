using System;
using System.Collections.Generic;

public class StatesFactory 
{
    private Dictionary<States, Type> _stateTypes;

    public StatesFactory()
    {
        _stateTypes = new Dictionary<States, Type>
        {
            { States.Idle, typeof(IdleState) },
            { States.Walk, typeof(WalkState) },
            { States.Jump, typeof(JumpState) }
        };
    }

    public State CreateState(States state)
    {
        if (_stateTypes.TryGetValue(state, out var type))
        {
            return (State)Activator.CreateInstance(type);
        }

        throw new ArgumentException($" Неизвестный стейт {state}");
    }
}
