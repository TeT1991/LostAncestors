using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : EntityState
{
    private readonly Character _entity;

    public JumpingState(Entity entity, StateMachine stateMachine) : base(entity, stateMachine)
    {
        _entity = entity as Character;
    }

    public override void Enter()
    {
        _entity.Jumper.Jump();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        _entity.Jumper.UpdatePosition(); 

        _entity.DirectionSwitcher.SetDirection(Input.GetAxis("Horizontal"));

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            _entity.DirectionSwitcher.SetDirection(Input.GetAxis("Horizontal"));
            _entity.Mover.Move(_entity.AirHorizontalSpeed * _entity.DirectionSwitcher.Direction);
        }

        SetCorrectJumpAnimation();
    }

    private void SetCorrectJumpAnimation()
    {
        string upAnimationName = "Jump_up";
        string downAnimationName = "Jump_down";

        if(_entity.Jumper.VerticalSpeed > 0)
        {
            _entity.AnimationSwitcher.TrySetAnimation(upAnimationName, true);
        }
        else
        {
            _entity.AnimationSwitcher.TrySetAnimation(downAnimationName, true);
        }
    }
}
