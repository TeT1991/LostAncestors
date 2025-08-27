using Spine.Unity;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private HoleDetector _holeDetector;
    [SerializeField] private SkeletonAnimation _skeletonAnimation;

    [SerializeField] private float _moveSpeed;

    private Rigidbody2D _rigidbody;
    private Rotater _rotater;
    private Mover _mover;
    private AnimationSwitcher _animationSwitcher;
    private int _direction;

    private void Awake()
    {
        Init();
    }

    private void FixedUpdate()
    {
        _mover.Move(_moveSpeed);
    }

    private void Init()
    {
        _direction = -1;

        _rigidbody = GetComponent<Rigidbody2D>();
        _mover = new(_rigidbody);
        _rotater = new(gameObject.transform);
        _animationSwitcher = new(_skeletonAnimation);
        _holeDetector.OnHoleDetected += SwitchDirection;
        _rotater.Rotate(_direction);
        _mover.SetDirection(_direction);
        _mover.AllowMove();
        _animationSwitcher.SetWalkAnimation();
    }

    private void SwitchDirection()
    {
        _direction *= -1;
        _rotater.Rotate(_direction);
        _mover.SetDirection(_direction);
    }
}
