public class WalkingState : EntityState
{
    private readonly Character _character;

    private readonly Mover _mover;
    private readonly DirectionSwitcher _directionSwitcher;

    public WalkingState(Entity entity, StateMachine stateMachine) : base(entity, stateMachine)
    {
        _character = entity as Character;
        _mover = _character.Mover;
        _directionSwitcher = _character.DirectionSwitcher;
    }

    public override void Enter()
    {
        string animationName = "Walk";
        _character.AnimationSwitcher.TrySetAnimation(animationName, true);
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        string animationName;
        if (_character.CollideDetector.IsWallCollided)
        {
            animationName = "Idle";

            _character.AnimationSwitcher.TrySetAnimation(animationName, true);
        }
        else
        {
            animationName = "Walk";
            _character.AnimationSwitcher.TrySetAnimation(animationName, true);
            _mover.Move(_character.GroundSpeed * _directionSwitcher.Direction);
        }
    }
}
