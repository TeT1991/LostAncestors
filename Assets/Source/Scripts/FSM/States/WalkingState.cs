public class WalkingState : EntityState
{
    private readonly Character _entity;

    private readonly Mover _mover;
    private readonly DirectionSwitcher _directionSwitcher;

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

        string animationName = "Walk";

        if (_entity.CollideDetector.IsWallCollided)
        {
            animationName = "Idle";

            _entity.AnimationSwitcher.TrySetAnimation(animationName, true);
        }
        else
        {
            animationName = "Walk";
            _entity.AnimationSwitcher.TrySetAnimation(animationName, true);
            _mover.Move(_entity.GroundSpeed * _directionSwitcher.Direction);
        }
    }
}
