using Spine.Unity;
using Spine.Unity.Examples;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(Rigidbody2D))]
public class Character : MonoBehaviour, IDamagable, ICoroutineRunner
{
    private readonly float _maxHealth = 100;
    private readonly float _currentHealth = 50;

    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private SkeletonAnimation _skeletonAnimation;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private PickableDetector _pickableDetector;
    [SerializeField] private Transform _projectileLaunchPoint;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _reloadTime;
    [SerializeField] private float _projectileSpeed;

    private WaitForSeconds _waitForAttackReload;
    private Coroutine _coroutine;

    private Jumper _jumper;
    private Rotater _rotater;
    private Mover _mover;
    private Atacker _atacker;
    private Health _health;
    private AnimationSwitcher _animationSwitcher;

    private int _direction;

    private bool _canMove;
    private bool _canJump;
    private bool _canAttack;

    private List<ButtonType> _commands;

    public event Action<float> HealthChanged;

    public Health Health => _health;

    private void Awake()
    {
        Init();
    }

    private void OnDestroy()
    {
        _inputReader.OnButtonPressed -= StartExecuteAction;
        _inputReader.OnButtonReleased -= StopExecuteAction;

        _groundDetector.OnGroundDetected -= AllowJump;
        _groundDetector.OnGroundNotDetected -= DenyJump;

        _pickableDetector.OnPicked -= TryPickUp;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Init()
    {
        _mover = new(_rigidBody, _moveSpeed);
        _jumper = new(_rigidBody, _jumpPower);
        _rotater = new(gameObject.transform);
        _animationSwitcher = new(_skeletonAnimation);
        _commands = new();
        _atacker = new(this,_projectileLaunchPoint, _projectilePrefab, _reloadTime);
        _health = new(_currentHealth, _maxHealth);
        _inputReader.Init();

        _direction = 1;

        _inputReader.OnButtonPressed += StartExecuteAction;
        _inputReader.OnButtonReleased += StopExecuteAction;

        _groundDetector.OnGroundDetected += AllowJump;
        _groundDetector.OnGroundNotDetected += DenyJump;

        _pickableDetector.OnPicked += TryPickUp;

        _waitForAttackReload = new WaitForSeconds(_reloadTime);

        AllowAttack();
    }

    private void StartExecuteAction(ButtonType buttonType)
    {
        switch (buttonType)
        {
            case ButtonType.WalkRight:
                ApplyMoveActions(1);
                break;

            case ButtonType.WalkLeft:
                ApplyMoveActions(-1);
                break;

            case ButtonType.Jump:
                Jump();
                break;

            case ButtonType.Attack:
                Attack();
                break;
        }
    }

    private void StopExecuteAction(ButtonType buttonType)
    {
        switch (buttonType)
        {
            case ButtonType.WalkRight:
            case ButtonType.WalkLeft:
                StopMove();
                break;
        }
    }

    private void TryPickUp(IPickable pickable)
    {
        if (pickable.GetPickableType() == PickableType.Medkit)
        {
            if (_health.CurrentHealth < _health.MaxHealth)
            {
                Heal();
                pickable.PickUp();
            }
        }
        else
        {
            pickable.PickUp();
        }
    }

    private void ApplyMoveActions(int direction)
    {
        _direction = direction;
        StartMove();

        if (_canJump)
        {
            _animationSwitcher.PlayWalkAnimation();
        }
    }

    private void StartMove()
    {
        _mover.SetDirection(_direction);
        _rotater.Rotate(_direction);
        _canMove = true;
    }

    private void StopMove()
    {
        _canMove = false;
        _rigidBody.velocity *= Vector2.up;
    }

    private void Move()
    {
        if (_canMove)
        {
            _mover.Move();
        }
        else
        {
            _animationSwitcher.PlayIdleAnimation();
        }
    }

    private void AllowJump()
    {
        _canJump = true;
    }

    private void DenyJump()
    {
        _canJump = false;
    }

    private void Jump()
    {
        if (_canJump)
        {
            _jumper.Jump();
            DenyJump();
            _animationSwitcher.PlayJumpAnimation();
        }
    }

    private void AllowAttack()
    {
        _canAttack = true;
    }

    private void DenyAttack()
    {
        _canAttack = false;
    }

    private void Attack()
    {
        _atacker.LaunchProjectile(_projectileSpeed, _direction);
        _coroutine = StartCoroutine(Reload());
        _animationSwitcher.PlayAttackAnimation();
        DenyAttack();
    }

    private IEnumerator Reload()
    {
        yield return _waitForAttackReload;

        AllowAttack();
    }

    private void Heal()
    {
        _health.IncreaseHealth();
        HealthChanged?.Invoke(_currentHealth);
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage()
    {
        _health.DecreaseHealth();
        HealthChanged?.Invoke(_currentHealth);

        if (_health.CurrentHealth <= 0)
        {
            Die();
        }
    }
}