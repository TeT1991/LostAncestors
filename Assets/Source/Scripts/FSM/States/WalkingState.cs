public class WalkingState : EntityState
{
    private Character _entity;

    private Mover _mover;
    private DirectionSwitcher _directionSwitcher;

    public WalkingState(Entity entity, StateMachine stateMachine) : base(entity, stateMachine)
    {
        _entity = entity as Character;
        _mover = _entity.Mover;
        _directionSwitcher = _entity.DirectionSwitcher;
    }

    public override void Enter()
    {
        string animationName = "Walk";
        _entity.AnimationSwitcher.TrySetAnimation(animationName, true);
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        _mover.Move(_entity.GroundSpeed * _directionSwitcher.Direction);
    }
}
