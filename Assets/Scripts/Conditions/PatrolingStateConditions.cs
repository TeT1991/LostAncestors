using UnityEngine;

public class PatrolingStateConditions : StateConditions
{
    private CharacterDetector _characterDetector;

    public PatrolingStateConditions(CharacterDetector characterDetector)
    {
        _stateType = EntityStates.Patroling;

        _allowedStates.Add(EntityStates.RangeAttack);

        _characterDetector = characterDetector;
    }

    public override bool CanChange(EntityStates currentState)
    {
        if (HasAllowedState(currentState))
        {
            if (_characterDetector.IsDetected == false)
            {
                return true;
            }
        }
        return false;
    }

}
