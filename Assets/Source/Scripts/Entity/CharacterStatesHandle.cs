using TMPro;
using UnityEngine;

public class CharacterStatesHandle : MonoBehaviour
{
    public TextMeshProUGUI _text;

    [SerializeField] private CharacterSwitcher _characterSwitcher;
    private Character _character;

    private bool _isWalking = false;
    private bool _isJumping = false;
    private bool _isAttacking = false;
    private bool _isClimbing = false;

    private StateMachine _stateMachine;
    private IdleState _idleState;
    private WalkingState _walkingState;
    private JumpingState _jumpingState;
    private AttackCharacterState _attackState;
    private ClimbingState _climbingState;

    private Conditions _idleConditions;
    private Conditions _walkConditions;
    private Conditions _jumpConditions;
    private Conditions _attackConditions;
    private Conditions _climbingConditons;

    private void Update()
    {
        TrySetState();
        ApplyStateActions();
        UpdateConditions();
        _text.text = _stateMachine.CurrentState.ToString();
    }

    public void Init(Character character)
    {
        _isWalking = false;
        _isJumping = false;
        _isAttacking = false;
        _isClimbing = false;

        _character = character;

        InitStateMachine();
        InitConditions();
    }

    public void SetJumpingStatus(bool value)
    {
        _isJumping = !value;
    }

    public void SetAttackStatus(bool value)
    {
        _isAttacking = value;
    }

    public void SetClimbingStatus(bool value)
    {
        _isClimbing = value;
    }

    private void TrySetAttackingState()
    {
        if (_attackConditions.IsConditionsCompleted())
        {
            string animationName = "RangeAttack";

            if (Input.GetMouseButtonDown(0))
            {
                _isAttacking = true;
                _attackState.SetAttackInfo(_character.MeleeProjectile, _character.ProjectileMeleeLaunchPoint, animationName);

                TryChangeState(_attackState);
            }

            if (Input.GetMouseButtonDown(1))
            {
                _isAttacking = true;
                _attackState.SetAttackInfo(_character.RangeProjectile, _character.ProjectileRangeLaunchPoint, animationName);

                TryChangeState(_attackState);
            }
        }
    }

    private void TrySetJumpingState()
    {
        if (_jumpConditions.IsConditionsCompleted())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _isJumping = true;

                TryChangeState(_jumpingState);
            }
        }
    }

    private void TrySetWalkintState()
    {
        if (_walkConditions.IsConditionsCompleted())
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
        if (_idleConditions.IsConditionsCompleted())
        {
            TryChangeState(_idleState);
        }
    }

    private void TrySetClimbingState()
    {
        if (_climbingConditons.IsConditionsCompleted())
        {
            if (_isClimbing)
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
                {
                    Debug.Log("!!!!");
                    TryChangeState(_climbingState);
                }
                else
                {
                    _isClimbing = false;
                }
            }

        }
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

    private void TrySetState()
    {
        TrySetIdleState();
        TrySetWalkintState();
        TrySetJumpingState();
        TrySetAttackingState();
        TrySetClimbingState();
    }

    private void UpdateConditions()
    {
        _idleConditions.UpdateConditionsStatus(_isWalking, _isJumping, _isAttacking, _isClimbing);
        _walkConditions.UpdateConditionsStatus(_isJumping, _isAttacking, _isClimbing);
        _jumpConditions.UpdateConditionsStatus(_isJumping, _isAttacking, _isClimbing);
        _attackConditions.UpdateConditionsStatus(_isAttacking, _isJumping, _isClimbing);
        _climbingConditons.UpdateConditionsStatus(_isAttacking, !_isClimbing);
    }

    private void InitConditions()
    {
        _idleConditions = new Conditions();
        _walkConditions = new Conditions();
        _jumpConditions = new Conditions();
        _attackConditions = new Conditions();
        _climbingConditons = new Conditions();

        UpdateConditions();
    }

    private void InitStateMachine()
    {
        _stateMachine = new StateMachine();
        _idleState = new IdleState(_character, _stateMachine);
        _walkingState = new WalkingState(_character, _stateMachine);
        _jumpingState = new JumpingState(_character, _stateMachine);
        _attackState = new AttackCharacterState(_character, _stateMachine);
        _climbingState = new ClimbingState(_character, _stateMachine);

        _stateMachine.Init(_idleState);
    }
}
