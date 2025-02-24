using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : EntityState
{
    public IdleState(Entity entity, StateMachine stateMachine, AnimationSwitcher animationSwitcher) : base(entity, stateMachine, animationSwitcher)
    {

    }

    public override void Enter()
    {
        string animationName = "Idle";

        base.Enter();
        AnimationSwitcher.SetAnimation(animationName, true);
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
