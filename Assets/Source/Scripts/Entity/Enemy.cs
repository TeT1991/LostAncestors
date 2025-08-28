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
    private Patroller _patroller;

    private EnemyState _state;
    private int _direction;
    private bool _canMove;

    private void Awake()
    {
        Init();
    }

    private void FixedUpdate()
    {
        ApplyStateActions();
    }

    private void OnDestroy()
    {
        _holeDetector.OnHoleDetected -= SwitchDirection;
    }

    private void Init()
    {
        _direction = -1;
        _canMove = false;

        _rigidbody = GetComponent<Rigidbody2D>();
        _mover = new(_rigidbody,_moveSpeed);
        _rotater = new(gameObject.transform);
        _animationSwitcher = new(_skeletonAnimation);
        _patroller = new(_mover);

        _holeDetector.OnHoleDetected += SwitchDirection;
        _rotater.Rotate(_direction);
        _mover.SetDirection(_direction);
        _animationSwitcher.SetWalkAnimation();
        AllowMove();
        _state = EnemyState.Patroling;
    }

    private void SwitchDirection()
    {
        _direction *= -1;
        _rotater.Rotate(_direction);
        _mover.SetDirection(_direction);
    }

    private void AllowMove()
    {
        _canMove = true;
    }

    private void ApplyStateActions()
    {
        switch ( _state)
        {
            case EnemyState.Patroling:
                _patroller.Patrol();
                break; 
        }
    }
}
