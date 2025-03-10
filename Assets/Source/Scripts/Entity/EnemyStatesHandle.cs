using UnityEngine;

public class EnemyStatesHandle : MonoBehaviour
{
    private Enemy _enemy;

    private bool _isPatroling = true;
    private bool _isAttacking = false;

    private StateMachine _stateMachine;
    private PatrolingState _patrolingState;
    private AttackEnemyState _attackingState;

    private Conditions _patrolingConditions;
    private Conditions _attackingConditions;

    private void Update()
    {
        TrySetState();
        ApplyStateActions();
        UpdateConditions();
    }

    public void Init(Enemy enemy)
    {
        _enemy = enemy;

        InitStateMachine();
        InitConditions();
    }

    private void TrySetAttackingState()
    {
        if (_attackingConditions.IsConditionsCompleted())
        {
            if (_enemy.EnemyCollideDetector.IsCharacterDetected == true)
            {
                _isAttacking = true;
                TryChangeState(_attackingState);
            }
        }
    }

    public void SetAttackStatus(bool value)
    {
        if (_enemy.EnemyCollideDetector.IsCharacterDetected == false)
        {
            _isAttacking = value;
        }
    }

    private void TrySetPatrolingState()
    {
        if (_patrolingConditions.IsConditionsCompleted())
        {
            if(_enemy.EnemyCollideDetector.IsCharacterDetected == false)
            {
                _isPatroling = true;

                TryChangeState(_patrolingState);
            }
            else
            {
                _isPatroling = false;
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

    private void TrySetState()
    {
        TrySetPatrolingState();
        TrySetAttackingState();
    }

    private void InitConditions()
    {
        _patrolingConditions = new Conditions();
        _attackingConditions = new Conditions();

        UpdateConditions();
    }

    private void UpdateConditions()
    {
        _patrolingConditions.UpdateConditionsStatus(_isAttacking);
        _attackingConditions.UpdateConditionsStatus(_isPatroling, _isAttacking);
    }

    private void InitStateMachine()
    {
        _stateMachine = new StateMachine();
        _patrolingState = new PatrolingState(_enemy,_stateMachine);
        _attackingState = new AttackEnemyState(_enemy,_stateMachine);

        _stateMachine.Init(_patrolingState);
    }

    private void ApplyStateActions()
    {
        _stateMachine.CurrentState.FrameUpdate();
    }
}
