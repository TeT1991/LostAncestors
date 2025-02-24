using UnityEngine;

public class PatrolingStateConditions : StateConditions
{
    private CharacterDetector _characterDetector;
    private Attacker _attacker;

    public PatrolingStateConditions(CharacterDetector characterDetector, Attacker attacker)
    {
        _stateType = EntityStates.Patroling;

        _allowedStates.Add(EntityStates.RangeAttack);

        _characterDetector = characterDetector;
        _attacker = attacker;
    }

    public override bool CanChange(EntityStates currentState)
    {
        if (HasAllowedState(currentState))
        {
            if (_characterDetector.IsDetected == false && _attacker.CanAttack)
            {
                return true;
            }
        }
        return false;
    }

}
