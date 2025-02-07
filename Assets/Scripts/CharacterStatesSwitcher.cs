using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatesSwitcher : MonoBehaviour
{
    private StateMachine _stateMachine;

    private void Update()
    {
        if(_stateMachine != null)
        {

       TrySwitchState();
        }
    }

    private  States GetState()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            return States.Walk;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            return States.Jump;
        }

        return States.Idle;
    }

    private void TrySwitchState()
    {
        var state = GetState();

        if (_stateMachine.GetCurrentStateType() != state)
        {
            _stateMachine.ChangState(state);
        }
    }

    public void Init(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }
}
