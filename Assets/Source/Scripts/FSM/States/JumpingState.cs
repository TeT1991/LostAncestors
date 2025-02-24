using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : EntityState
{
    private Character _character;

    private Mover _mover;
    private DirectionSwitcher _directionSwitcher;
    private Jumper _jumper;
    private float _horizontalSpeed;

    public JumpingState(Entity entity, StateMachine stateMachine) : base(entity, stateMachine)
    {
        _character = entity as Character;
        _mover = _character.Mover;
        _directionSwitcher = _character.DirectionSwitcher;
        _jumper = _character.Jumper;
    }

    public override void Enter()
    {
        base.Enter();

        _jumper.Jump();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        _directionSwitcher.SetDirection(Input.GetAxis("Horizontal"));

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            _character.DirectionSwitcher.SetDirection(Input.GetAxis("Horizontal"));
            _mover.Move(_character.AirHorizontalSpeed * _character.DirectionSwitcher.Direction);

        }

        SetCorrectJumpAnimation();
    }

    private void SetCorrectJumpAnimation()
    {
        string upAnimationName = "Jump_up";
        string downAnimationName = "Jump_down";

        if(_jumper.VerticalSpeed > 0)
        {
            AnimationSwitcher.TrySetAnimation(upAnimationName, true);
        }
        else
        {
            AnimationSwitcher.TrySetAnimation(downAnimationName, true);
        }
    }
}
