using System.Collections.Generic;

public class StateConditions
{
    protected EntityStates _stateType;
    protected List<EntityStates> _allowedStates;

    private bool _isAviable;

    public EntityStates Type => _stateType;
    public bool IsAviable => _isAviable;

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

    public void SetAviable()
    {
        _isAviable = true;
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
