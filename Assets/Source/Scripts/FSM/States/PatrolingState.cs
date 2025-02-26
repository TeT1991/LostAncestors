public class PatrolingState : EntityState
{
    private Enemy _enemy;

    public PatrolingState(Entity entity, StateMachine stateMachine) : base(entity, stateMachine)
    {
        _enemy = entity as Enemy;
    }

    public override void Enter()
    {
        string animationName = "Walk";
        _enemy.AnimationSwitcher.TrySetAnimation(animationName, true);
    }

    public override void FrameUpdate()
    {
        _enemy.Mover.Move(_enemy.GroundSpeed * _enemy.DirectionSwitcher.Direction);
    }
}
