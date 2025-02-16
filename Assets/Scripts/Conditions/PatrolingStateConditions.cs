using UnityEngine;

public class PatrolingStateConditions : StateConditions
{
    public PatrolingStateConditions()
    {
        _stateType = EntityStates.Patroling;

        _allowedStates.Add(EntityStates.RangeAttack);
    }
}
