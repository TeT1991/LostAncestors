using Spine.Unity;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected EntityConfig _config;
    [SerializeField] protected SkeletonAnimation _skeletonAnimation;

    public List<StateConditions> Conditions { get; protected set; }
    protected EntityStates CurrentState;

    private StateMachine _stateMachine;

    private void Awake()
    {
        Init();
    }

    protected virtual void Update()
    {
        _stateMachine.CurrentState.FrameUpdate();
    }

    protected virtual void Init()
    {
        LoadConfig();
        InitComponents();
        InitStates();

        Conditions = new List<StateConditions>
        {
        };
    }

    protected virtual void SwitchState() 
    {
        foreach (var condition in Conditions)
        {
            if (condition.CanChange(CurrentState))
            {
                CurrentState = condition.Type;
                return;
            }
        }
    }

    protected virtual void ApplyIdleActions() { }
    protected virtual void ApplyStateActions() { }
    protected virtual void ApplyWalkStateActions() { }
    protected virtual void ApplyJumpStateActions() { }
    protected virtual void ApplyRangeAttackStateActions() { }
    protected virtual void ApplyPatrolingStateActions() { }
    protected virtual void LoadConfig() { }
    protected virtual void InitComponents() { }
    protected virtual void InitStates()
    {
        _stateMachine = new StateMachine();
    }

    protected void FlipSprites(float direction)
    {
        float negativeScale = -1;
        float positiveScale = 1;
        float scaleX = direction <= 0 ? negativeScale : positiveScale;
        _skeletonAnimation.Skeleton.ScaleX = scaleX;
    }
}
