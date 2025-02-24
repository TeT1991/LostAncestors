using System.Collections.Generic;

public class StateConditions
{
    protected EntityStates _stateType;
    protected List<EntityStates> _allowedStates;

    public EntityStates Type => _stateType;

    public StateConditions()
    {
        _allowedStates = new List<EntityStates>();
    }

    public virtual bool CanChange(EntityStates currenState)
    {
        if (_allowedStates.Contains(EntityStates.Any))
        {
            return true;
        }

        return false;
    }

    protected bool HasAllowedState(EntityStates currentState)
    {
        bool isAlowedState = false;

        foreach (var state in _allowedStates)
        {
            if (state == currentState)
            {
                isAlowedState = true;
                break;
            }
        }

        return isAlowedState;
    }
}
