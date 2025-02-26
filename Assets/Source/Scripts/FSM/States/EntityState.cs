public class EntityState 
{
    protected Entity Entity;
    protected StateMachine StateMachine;

    public EntityState(Entity entity, StateMachine stateMachine)
    {
        Entity = entity;
        StateMachine = stateMachine;
    }

    public virtual void Enter() { }
    public virtual void FrameUpdate() { }
    public virtual void Exit() { }
}
