using UnityEngine;

public class EntityEnemy : Entity
{
    [SerializeField] private HoleDetector _holeDetector;
    private EntityRotater _entityRotater;
    private int _direction;

    protected override void Init()
    {
        base.Init();
        string walkAnimationName = "Walk";

        _direction = -1;
        _entityRotater = new(this);
        _holeDetector.OnHoleDetected += SwitchDirection;
        _entityRotater.Rotate(_direction);
        EntityMover.SetDirection(_direction);
        EntityMover.AllowMove();
        AnimationSwitcher.SetAnimation(walkAnimationName, true);
    }

    private void SwitchDirection()
    {
        _direction *= -1;
        _entityRotater.Rotate(_direction);
        EntityMover.SetDirection(_direction);
    }
}
