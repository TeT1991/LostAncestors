public class EntityState 
{
    protected Entity Entity;
    protected StateMachine StateMachine;
    protected AnimationSwitcher AnimationSwitcher;

    public EntityState(Entity entity, StateMachine stateMachine, AnimationSwitcher animationSwitcher)
    {
        Entity = entity;
        StateMachine = stateMachine;
        AnimationSwitcher = animationSwitcher;
    }

    public virtual void Enter() { }
    public virtual void FrameUpdate() { }
    public virtual void Exit() { }
}
