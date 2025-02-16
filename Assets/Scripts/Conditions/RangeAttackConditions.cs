using UnityEngine;

public class RangeAttackConditions : StateConditions
{
    private readonly Attacker _attacker;

    public RangeAttackConditions(Attacker attacker)
    {
        _stateType = EntityStates.RangeAttack;
        _allowedStates.Add(EntityStates.Idle);
        _allowedStates.Add(EntityStates.Walk);

        _attacker = attacker;
    }

    public override bool CanChange(EntityStates currentState)
    {
        if (HasAllowedState(currentState))
        {
            if (_attacker.gameObject.TryGetComponent<Character>(out var character))
            {
                if (_attacker.CanAttack && Input.GetKey(KeyCode.Mouse1))
                {
                    return true;
                }
            }
            else if (_attacker.gameObject.TryGetComponent<Enemy>(out var enemy))
            {

            }
        }
        return false;
    }
}

