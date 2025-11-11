using Spine.Unity;
using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] private HoleDetector _holeDetector;
    [SerializeField] private CharacterDetector _characterDetector;
    [SerializeField] private SkeletonAnimation _skeletonAnimation;

    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private Transform _projectileSpawnPoint;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _reloadTime;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _attackDistance;

    private Transform _chasingObject;

    private Rigidbody2D _rigidbody;
    private Rotater _rotater;
    private Mover _mover;
    private AnimationSwitcher _animationSwitcher;
    private Patroller _patroller;
    private Atacker _atacker;
    private Health _health;

    private Coroutine _coroutine;
    private WaitForSeconds _waitForAttackReload;

    private int _direction;
    private int _currentHealth = 1;
    private bool _canMove;
    private bool _canAttack;

    public event Action<Enemy> OnHealthOver;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        SelectAction();
    }

    private void OnDestroy()
    {
        _holeDetector.OnHoleDetected -= SwitchDirection;
        _characterDetector.OnDetected -= SetChasingObject;
        _characterDetector.OnNotDetected -= ResetCharacter;
    }

    private void SelectAction()
    {
        if (_chasingObject == null)
        {
            _mover.Move();
        }
        else
        {
            Attack();
        }
    }

    private void Init()
    {
        _direction = -1;
        _canMove = false;
        _canAttack = true;

        _rigidbody = GetComponent<Rigidbody2D>();
        _mover = new(_rigidbody, _moveSpeed);
        _rotater = new(gameObject.transform);
        _animationSwitcher = new(_skeletonAnimation);
        _patroller = new(_mover);
        _atacker = new(this,_projectileSpawnPoint, _projectilePrefab, _reloadTime);
        _health = new(_currentHealth, _currentHealth);

        _holeDetector.OnHoleDetected += SwitchDirection;
        _characterDetector.OnDetected += SetChasingObject;
        _characterDetector.OnNotDetected += ResetCharacter;
        _rotater.Rotate(_direction);
        _mover.SetDirection(_direction);
        _characterDetector.Init(_attackDistance);
        _animationSwitcher.PlayWalkAnimation();
        AllowMove();

        _waitForAttackReload = new WaitForSeconds(_reloadTime);
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

    private void Attack()
    {
        _atacker.LaunchProjectile(_projectileSpeed, _direction);
    }

    private void SetChasingObject(Transform chasigObject)
    {
        _chasingObject = chasigObject;
    }

    private void ResetCharacter()
    {
        _chasingObject = null;
    }

    private void Die()
    {
        OnHealthOver?.Invoke(this);
    }

    public void TakeDamage()
    {
        _health.DecreaseHealth();

        if (_health.CurrentHealth <= 0)
        {
            Die();
        }
    }
}
