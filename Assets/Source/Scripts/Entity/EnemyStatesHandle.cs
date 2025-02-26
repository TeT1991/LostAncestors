using UnityEngine;

public class EnemyStatesHandle : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;

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

        text.text = _stateMachine.CurrentState.ToString();  
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
            if (_enemy.CharacterDetector.IsDetected == true)
            {
                _isAttacking = true;
                TryChangeState(_attackingState);
            }
        }
    }

    public void SetAttackStatus(bool value)
    {
        if (_enemy.CharacterDetector.IsDetected == false)
        {
            Debug.Log("!!!");
            _isAttacking = value;
        }
    }

    private void TrySetPatrolingState()
    {
        Debug.Log(_patrolingConditions.ToString());

        if (_patrolingConditions.IsConditionsCompleted())
        {
            if(_enemy.CharacterDetector.IsDetected == false)
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
