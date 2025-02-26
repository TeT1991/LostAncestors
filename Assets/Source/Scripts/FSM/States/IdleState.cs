public class IdleState : EntityState
{
    private readonly Entity _entity;

    public IdleState(Entity entity, StateMachine stateMachine) : base(entity, stateMachine) 
    {
        _entity = entity;
    }

    public override void Enter()
    {
        string animationName = "Idle";

        _entity.AnimationSwitcher.TrySetAnimation(animationName, true);
    }
}
