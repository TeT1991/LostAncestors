using Spine.Unity;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected SkeletonAnimation SkeletonAnimation;
    [SerializeField] protected float MoveSpeed;
    protected AnimationSwitcher AnimationSwitcher;

    protected EntityMover EntityMover;

    private void Awake()
    {
        Init();
    }

    protected virtual void Update()
    {
        EntityMover.Move(MoveSpeed);
    }

    protected virtual void Init()
    {
        EntityMover = new(this);
        AnimationSwitcher = new(SkeletonAnimation);
    }
}
