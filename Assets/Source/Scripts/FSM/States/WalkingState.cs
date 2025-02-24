using UnityEngine;

public class WalkingState : EntityState
{
    private Character _character;

    private Mover _mover;
    private DirectionSwitcher _directionSwitcher;

    public WalkingState(Entity entity, StateMachine stateMachine) : base(entity, stateMachine)
    {
        _character = entity as Character;
        _mover = _character.Mover;
        _directionSwitcher = _character.DirectionSwitcher;
    }

    public override void Enter()
    {
        base.Enter();

        string animationName = "Walk";

        AnimationSwitcher.TrySetAnimation(animationName, true);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        _mover.Move(_character.GroundSpeed * _directionSwitcher.Direction);
    }

    private void TrySetAnimation()
    {

    }
}
