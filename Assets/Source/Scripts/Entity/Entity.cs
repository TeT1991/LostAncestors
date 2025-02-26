using Spine;
using Spine.Unity;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimationSwitcher))]
public class Entity : MonoBehaviour
{
    [SerializeField] protected EntityConfig Config;
    [SerializeField] protected SkeletonAnimation SkeletonAnimation;

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

    protected virtual void LoadConfig() { }

    protected virtual void InitComponents()
    {
        _animationSwitcher = GetComponent<AnimationSwitcher>();
        _animationSwitcher.Init(SkeletonAnimation);
    }

    protected void FlipSprites(float direction)
    {
        float negativeScale = -1;
        float positiveScale = 1;
        float scaleX = direction <= 0 ? negativeScale : positiveScale;
        SkeletonAnimation.Skeleton.ScaleX = scaleX;
    }
}
