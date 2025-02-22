using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Jumper), typeof(Attacker))]
[RequireComponent(typeof(CollideDetector), typeof(DirectionSwitcher), typeof(AnimationSwitcher))]
public class Character : Entity
{
    private float _groundSpeed;
    private float _airHorizontalSpeed;
    private float _jumpPower;
    private float _reloadTime;
    private Transform _projectile;

    private Mover _mover;
    private Jumper _jumper;
    private Attacker _attacker;
    private CollideDetector _collideDetector;
    private DirectionSwitcher _directionSwitcher;
    private AnimationSwitcher _animationSwitcher;

    protected override void Init()
    {
        base.Init();

        Conditions = new List<StateConditions>
        {
            new IdleStateConditions(_collideDetector),
            new WalkStateConditions(),
            new JumpStateConditions(_jumper),
            new CharacterRangeAttackConditions(_attacker)
        };
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void ApplyStateActions()
    {
        switch (CurrentState)
        {
            case EntityStates.Idle:
                ApplyIdleActions();
                break;

            case EntityStates.Walk:
                ApplyWalkStateActions();
                break;

            case EntityStates.Jump:
                ApplyJumpStateActions();
                break;

            case EntityStates.RangeAttack:
                ApplyRangeAttackStateActions();
                break;
        }
    }

    protected override void ApplyIdleActions()
    {
        _animationSwitcher.SetAnimation("Idle", true);
    }

    protected override void ApplyWalkStateActions()
    {
        _directionSwitcher.SetDirection(Input.GetAxis("Horizontal"));
        _mover.Move(_groundSpeed * _directionSwitcher.Direction);
        _animationSwitcher.SetAnimation("Walk", true);
    }

    protected override void ApplyJumpStateActions()
    {
        _jumper.Jump();
        _directionSwitcher.SetDirection(Input.GetAxis("Horizontal"));
        _mover.Move(_airHorizontalSpeed * Input.GetAxis("Horizontal"));
   
        if (_jumper.CurrentVerticalSpeed > 0)
        {
            _animationSwitcher.SetAnimation("Jump_up", true);
        }
        else
        {
            _animationSwitcher.SetAnimation("Jump_down", true);
        }
    }

    protected override void LoadConfig()
    {
        CurrentState = _config.State;
        _groundSpeed = _config.GroundSpeed;
        _airHorizontalSpeed = _config.AirHorizontalSpeed;
        _jumpPower = _config.JumpPower;
        _reloadTime = _config.ReloadTime;
        _projectile = _config.Projectile;
    }

    protected override void InitComponents()
    {
        _mover = GetComponent<Mover>();
        _jumper = GetComponent<Jumper>();
        _attacker = GetComponent<Attacker>();
        _collideDetector = GetComponent<CollideDetector>();
        _directionSwitcher = GetComponent<DirectionSwitcher>();
        _animationSwitcher = GetComponent<AnimationSwitcher>();

        _jumper.Init(GetComponent<Rigidbody2D>(), _jumpPower);
        _attacker.Init(_projectile, _reloadTime);
        _directionSwitcher.Init(_config.StartDirection);
        _animationSwitcher.Init(_skeletonAnimation);

        _directionSwitcher.DirectionChanged += FlipSprites;
        _collideDetector.PlatformCollided += _jumper.SetStatus;
    }
}

