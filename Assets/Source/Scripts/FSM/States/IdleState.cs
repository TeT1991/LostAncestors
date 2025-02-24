public class IdleState : EntityState
{
    public IdleState(Entity entity, StateMachine stateMachine) : base(entity, stateMachine)
    {
    }

    public override void Enter()
    {
        string animationName = "Idle";

        base.Enter();
        AnimationSwitcher.TrySetAnimation(animationName, true);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }
}
