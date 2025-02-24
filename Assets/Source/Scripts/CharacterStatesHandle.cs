using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatesHandle : MonoBehaviour
{
    public TMPro.TextMeshProUGUI m_TextMeshPro;

    [SerializeField] private CharacterSwitcher _characterSwitcher;
    private Character _character;

    private bool _isIdle = true;
    private bool _isWalking = false;
    private bool _isJumping = false;

    private StateMachine _stateMachine;
    private IdleState _idleState;
    private WalkingState _walkingState;
    private JumpingState _jumpingState;

    private List<Func<bool>> _idleConditions;
    private List<Func<bool>> _walkConditions;
    private List<Func<bool>> _jumpConditions;

    public bool IsWalking => _isWalking;

    public void Init(Character character)
    {
        _character = character;
        _isJumping = false;

        _character.CollideDetector.PlatformCollided += SetJumpingStatus;

        InitStateMachine();
        InitConditions();
    }

    private void Update()
    {
        m_TextMeshPro.text = (_stateMachine.CurrentState).ToString();
        TrySetIdleState();
        TrySetWalkintState();
        TrySetJumpingState();
        ApplyStateActions();
    }

    public void TrySetJumpingState()
    {
        if (IsConditionsCompleted(_jumpConditions))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _isJumping = true;
                TryChangeState(_jumpingState);
            }
        }
    }

    public void SetJumpingStatus(bool value)
    {
        _isJumping = !value;
    }

    public void TrySetWalkintState()
    {
        if (IsConditionsCompleted(_walkConditions))
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                _isWalking = true;

                _character.DirectionSwitcher.SetDirection(Input.GetAxis("Horizontal"));

                TryChangeState(_walkingState);
            }
            else
            {
                _isWalking = false;
            }
        }
    }

    private void TrySetIdleState()
    {
        if (IsConditionsCompleted(_idleConditions))
        {
            TryChangeState(_idleState);
        }
    }

    private bool IsConditionsCompleted(List<Func<bool>> conditions)
    {
        bool conditinsCompleted = false;

        foreach (var condition in conditions)
        {
            conditinsCompleted = !condition();

            if (conditinsCompleted == false)
            {
                break;
            }
        }

        return conditinsCompleted;
    }

    private void TryChangeState(EntityState state)
    {
        if (_stateMachine.CurrentState != state)
        {
            _stateMachine.ChangeState(state);
        }
    }

    private void ApplyStateActions()
    {
        _stateMachine.CurrentState.FrameUpdate();
    }

    private void InitConditions()
    {
        InitIdleConditions();
        InitWalkConditions();
        InitJumpConditions();
    }

    private void InitIdleConditions()
    {
        _idleConditions = new()
        {
            () => _isWalking,
            () => _isJumping
        };
    }

    private void InitWalkConditions()
    {
        _walkConditions = new()
        {
            () => _isJumping,
        };
    }

    private void InitJumpConditions()
    {
        _jumpConditions = new()
        {
            () => _isJumping,
        };
    }

    private void InitStateMachine()
    {
        _stateMachine = new StateMachine();
        _idleState = new IdleState(_character, _stateMachine);
        _walkingState = new WalkingState(_character, _stateMachine);
        _jumpingState = new JumpingState(_character, _stateMachine);

        _stateMachine.Init(_idleState);
    }
}
