using Spine.Unity;

public class AnimationSwitcher
{
    private readonly SkeletonAnimation _skeletonAnimation;
    private readonly string _walkAnimationName = "Walk";
    private readonly string _jumpAnimationName = "Jump_up";
    private readonly string _idleAnimationName = "Idle";
    private readonly string _attackAnimationName = "Attack";

    public AnimationSwitcher(SkeletonAnimation skeletonAnimation)
    {
        _skeletonAnimation = skeletonAnimation;
        _skeletonAnimation.Initialize(true);
    }

    public void PlayIdleAnimation()
    {
        bool isLooping = true;
        SetAnimation(_idleAnimationName, isLooping);
    }

    public void PlayAttackAnimation()
    {
        bool isLooping = false;
        SetAnimation(_attackAnimationName, isLooping);
        _skeletonAnimation.state.AddAnimation(0, _idleAnimationName, true, 0);
    }

    public void PlayWalkAnimation()
    {
        bool isLooping = true;
        SetAnimation(_walkAnimationName, isLooping);
    }

    public void PlayJumpAnimation()
    {
        bool isLooping = true;
        SetAnimation(_jumpAnimationName, isLooping);
    }

    private void SetAnimation(string animationName, bool isLooping)
    {
        if (_skeletonAnimation.AnimationName != animationName)
        {
            int stateIndex = 0;

            _skeletonAnimation.state.SetAnimation(stateIndex, animationName, isLooping);
        }
    }
}
