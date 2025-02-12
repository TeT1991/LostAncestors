using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpStateConditions : StateConditions
{
    private Jumper _jumper;
    private bool _isJumping;


    public JumpStateConditions(Jumper jumper)
    {
        _jumper = jumper;

        _stateType = EntityStates.Jump;

        _allowedStates.Add(EntityStates.Idle);
        _allowedStates.Add(EntityStates.Walk);
        _jumper = jumper;
    }

    public override bool CanChange(EntityStates currenState)
    {
        if (_jumper.IsGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            return true;
        }

        return false;
    }
}
