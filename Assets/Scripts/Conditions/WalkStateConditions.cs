using UnityEngine;

public class WalkStateConditions : StateConditions
{
    public WalkStateConditions()
    {
        _stateType = EntityStates.Walk;
        _allowedStates.Add(EntityStates.Idle);
    }

    public override bool CanChange(EntityStates currentState)
    {
        if (HasAllowedState(currentState))
        {
            if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                return true;
            }
        }

        return false;
    }
}
