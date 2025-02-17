using UnityEngine;

public class EnemyRangeAttackConditions : StateConditions
{
    private readonly Attacker _attacker;
    private CharacterDetector _characterDetector;

    public EnemyRangeAttackConditions(Attacker attacker,CharacterDetector characterDetector)
    {
        _stateType = EntityStates.RangeAttack;
        _allowedStates.Add(EntityStates.Patroling);

        _attacker = attacker;
        _characterDetector = characterDetector;
    }

    public override bool CanChange(EntityStates currentState)
    {
        if (HasAllowedState(currentState))
        {
            if (_characterDetector.IsDetected)
            {
                return true;
            }
            if(_attacker.CanAttack == false)
            {
                return true;
            }
        }
        return false;
    }
}

