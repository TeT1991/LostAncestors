using System.Collections.Generic;
using UnityEngine;

public class IdleStateConditions : StateConditions
{
    private readonly CollideDetector _detector;

    private bool _isGrounded;

    public IdleStateConditions(CollideDetector detector)
    {
        _stateType = EntityStates.Idle;
        _allowedStates.Add(EntityStates.Walk);
        _allowedStates.Add(EntityStates.Jump);
        _allowedStates.Add(EntityStates.RangeAttack);

        _detector = detector;
        _detector.PlatformCollided += SetIsGroundedStatus;
    }

    public override bool CanChange(EntityStates currenState)
    {
        if (Input.anyKey == false && _isGrounded)
        {
            return true;
        }

        return false;
    }

    private void SetIsGroundedStatus(bool value)
    {
        _isGrounded = value;
    }
}
