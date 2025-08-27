using Spine.Unity;

public class AnimationSwitcher
{
    private readonly SkeletonAnimation _skeletonAnimation;
    private readonly string _walkAnimationName = "Walk";
    private readonly string _jumpAnimationName = "Jump_up";
    private readonly string _idleAnimationName = "Idle";

    public AnimationSwitcher(SkeletonAnimation skeletonAnimation)
    {
        _skeletonAnimation = skeletonAnimation;
        _skeletonAnimation.Initialize(true);
    }

    public void SetIdleAnimation()
    {
        bool isLooping = true;

        if (_skeletonAnimation.AnimationName != _idleAnimationName)
        {
            int stateIndex = 0;

            _skeletonAnimation.state.SetAnimation(stateIndex, _idleAnimationName, isLooping);
        }
    }

    public void SetWalkAnimation()
    {
        bool isLooping = true;

        if (_skeletonAnimation.AnimationName != _walkAnimationName)
        {
            int stateIndex = 0;

            _skeletonAnimation.state.SetAnimation(stateIndex, _walkAnimationName, isLooping);
        }
    }

    public void SetJumpAnimation()
    {
        bool isLooping = true;

        if (_skeletonAnimation.AnimationName != _jumpAnimationName)
        {
            int stateIndex = 0;

            _skeletonAnimation.state.SetAnimation(stateIndex, _jumpAnimationName, isLooping);
        }
    }
}
