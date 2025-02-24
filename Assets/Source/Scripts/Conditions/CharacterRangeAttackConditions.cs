using UnityEngine;

public class CharacterRangeAttackConditions : StateConditions
{
    private readonly Attacker _attacker;

    public CharacterRangeAttackConditions(Attacker attacker)
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
            if (_attacker.CanAttack && Input.GetKey(KeyCode.Mouse1))
            {
                return true;
            }
        }
        return false;
    }
}

