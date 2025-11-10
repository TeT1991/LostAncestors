using Spine.Unity;
using System.Collections;
using Unity.VisualScripting;
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
    [SerializeField] private float _chasingDistance;
    [SerializeField] private float _attackDistance;

    private Transform _chasingObject;

    private Rigidbody2D _rigidbody;
    private Rotater _rotater;
    private Mover _mover;
    private AnimationSwitcher _animationSwitcher;
    private Patroller _patroller;
    private Atacker _atacker;

    private Coroutine _coroutine;
    private WaitForSeconds _waitForAttackReload;

    private EnemyState _state;
    private int _direction;
    private int _health = 1;
    private bool _canMove;
    private bool _canAttack;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        ApplyStateActions();
    }

    private void OnDestroy()
    {
        _holeDetector.OnHoleDetected -= SwitchDirection;
        _characterDetector.OnDetected -= SetChasingObject;
        _characterDetector.OnNotDetected -= ResetCharacter;
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
        _atacker = new(_projectileSpawnPoint, _projectilePrefab);

        _holeDetector.OnHoleDetected += SwitchDirection;
        _characterDetector.OnDetected += SetChasingObject;
        _characterDetector.OnNotDetected += ResetCharacter;
        _rotater.Rotate(_direction);
        _mover.SetDirection(_direction);
        _characterDetector.Init(_chasingDistance);
        _animationSwitcher.PlayWalkAnimation();
        AllowMove();
        _state = EnemyState.Patroling;

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

    private void ApplyStateActions()
    {
        switch (_state)
        {
            case EnemyState.Patroling:
                _patroller.Patrol();
                break;

            case EnemyState.Chasing:
                ApplyChasingActions();
                break;
        }
    }

    private void ApplyChasingActions()
    {
        if(_chasingObject == null)
        {
            return;
        }

        float distance = Vector2.Distance(transform.position, _chasingObject.transform.position);

        if (distance >= _attackDistance)
        {
            _mover.Move();
            _animationSwitcher.PlayWalkAnimation();
        }
        else if (distance <= _attackDistance)
        {
            if (_canAttack)
            {
                Attack();
                _canAttack = false;
                _coroutine = StartCoroutine(Reload());
                _animationSwitcher.PlayAttackAnimation();
            }
            else
            {
                _animationSwitcher.PlayWalkAnimation();
            }
        }

    }

    private void Attack()
    {
        _atacker.LaunchProjectile(_projectileSpeed, _direction);

    }

    private void SetChasingObject(Transform chasigObject)
    {
        _chasingObject = chasigObject;
        SetChasingState();
    }

    private void ResetCharacter()
    {
        SetPatrolingState();
        _chasingObject = null;
    }

    private void SetPatrolingState()
    {
        _state = EnemyState.Patroling;
    }

    private void SetChasingState()
    {
        _state = EnemyState.Chasing;
    }

    private IEnumerator Reload()
    {
        yield return _waitForAttackReload;

        _canAttack = true;
    }

    private void DecreaseHealth()
    {
        _health--;
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage()
    {
        DecreaseHealth();

        if (_health <= 0)
        {
            Die();
        }
    }
}
