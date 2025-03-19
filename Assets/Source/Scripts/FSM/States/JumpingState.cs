using UnityEngine;

public class JumpingState : EntityState
{
    private readonly Character _character;

    public JumpingState(Entity entity, StateMachine stateMachine) : base(entity, stateMachine)
    {
        _character = entity as Character;
    }

    public override void Enter()
    {
        _character.Jumper.Jump();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        _character.DirectionSwitcher.SetDirection(Input.GetAxis("Horizontal"));

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            _character.DirectionSwitcher.SetDirection(Input.GetAxis("Horizontal"));

            if (_character.CollideDetector.IsWallCollided == false)
            {
                _character.Mover.Move(_character.AirHorizontalSpeed * _character.DirectionSwitcher.Direction);
            }
        }

        SetCorrectJumpAnimation();
    }

    private void SetCorrectJumpAnimation()
    {
        string upAnimationName = "Jump_up";
        string downAnimationName = "Jump_down";

        if (_character.Jumper.VerticalSpeed > 0)
        {
            _character.AnimationSwitcher.TrySetAnimation(upAnimationName, true);
        }
        else
        {
            _character.AnimationSwitcher.TrySetAnimation(downAnimationName, true);
        }
    }
}
