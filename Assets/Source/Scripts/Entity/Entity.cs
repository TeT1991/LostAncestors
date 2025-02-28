using Spine.Unity;
using UnityEngine;

[RequireComponent(typeof(AnimationSwitcher), typeof(HealthHandler))]
public class Entity : MonoBehaviour
{
    [SerializeField] protected EntityConfig Config;
    [SerializeField] protected SkeletonAnimation SkeletonAnimation;
    protected HealthHandler HealthHandler;
    protected OwnerType OwnerType;

    private int _health;

    private AnimationSwitcher _animationSwitcher;

    public AnimationSwitcher AnimationSwitcher => _animationSwitcher;

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        LoadConfig();
        InitComponents();
    }

    protected virtual void LoadConfig() 
    {
        _health = Config.Health;
    }

    protected virtual void InitComponents()
    {
        _animationSwitcher = GetComponent<AnimationSwitcher>();
        HealthHandler = GetComponent<HealthHandler>();
        
        SkeletonAnimation.Initialize(true);
        _animationSwitcher.Init(SkeletonAnimation);
        HealthHandler.Init(_health);
    }

    protected void FlipSprites(float direction)
    {
        float negativeScale = -1;
        float positiveScale = 1;
        float scaleX = direction <= 0 ? negativeScale : positiveScale;
        SkeletonAnimation.Skeleton.ScaleX = scaleX;
    }
}
