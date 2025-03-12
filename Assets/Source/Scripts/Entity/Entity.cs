using Spine.Unity;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected EntityConfig Config;
    [SerializeField] protected SkeletonAnimation SkeletonAnimation;
    protected OwnerType OwnerType;

    private int _health;

    private AnimationSwitcher _animationSwitcher;
    private HealthHandler _healthHandler;

    public AnimationSwitcher AnimationSwitcher => _animationSwitcher;
    public HealthHandler HealthHandler => _healthHandler;

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
        _animationSwitcher = new AnimationSwitcher();
        _healthHandler = new HealthHandler();

        SkeletonAnimation.Initialize(true);
        _animationSwitcher.Init(SkeletonAnimation);
        HealthHandler.Init(_health);

        _healthHandler.Died += Destroy;
    }

    protected void FlipSprites(float direction)
    {
        float negativeScale = -1;
        float positiveScale = 1;
        float scaleX = direction <= 0 ? negativeScale : positiveScale;
        SkeletonAnimation.Skeleton.ScaleX = scaleX;
    }

    protected void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        _healthHandler.Died -= Destroy;
    }
}
