using UnityEngine;

public class EntityState 
{
    protected Entity Entity;
    protected StateMachine StateMachine;
    protected AnimationSwitcher AnimationSwitcher;

    public EntityState(Entity entity, StateMachine stateMachine)
    {
        Entity = entity;
        StateMachine = stateMachine;
        AnimationSwitcher = entity.AnimationSwitcher;
    }

    public virtual void Enter() { }
    public virtual void FrameUpdate() { }
    public virtual void Exit() { }
}
