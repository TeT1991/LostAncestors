using Spine.Unity;

public class AnimationSwitcher
{
    private readonly SkeletonAnimation _skeletonAnimation;

    public AnimationSwitcher(SkeletonAnimation skeletonAnimation)
    {
        _skeletonAnimation = skeletonAnimation;
    }

    public void SetAnimation(string animationName, bool isLooping)
    {
        if (_skeletonAnimation.AnimationName != animationName)
        {
            int stateIndex = 0;

            _skeletonAnimation.state.SetAnimation(stateIndex, animationName, isLooping);
        }
    }
}
