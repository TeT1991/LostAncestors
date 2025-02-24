using Spine.Unity;
using UnityEngine;

public class AnimationSwitcher : MonoBehaviour
{
    private SkeletonAnimation _skeletonAnimation;

    public  void Init(SkeletonAnimation skeletonAnimation)
    {
        _skeletonAnimation = skeletonAnimation;
    }

    public void TrySetAnimation(string animationName, bool isLoop)
    {
        int trackIndex = 0;

        if (animationName != _skeletonAnimation.AnimationName)
        {
            _skeletonAnimation.state.SetAnimation(trackIndex, animationName, isLoop);
        }
    }
}
