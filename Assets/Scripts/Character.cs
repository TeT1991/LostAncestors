using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Jumper), typeof(Attacker))]
[RequireComponent(typeof(CollideDetector), typeof(DirectionSwitcher))]
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
        _textMeshPro.text = CurrentState.ToString();
    }

    protected override void ApplyStateActions()
    {
        switch (CurrentState)
        {
            case EntityStates.Idle:
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

    protected override void ApplyWalkStateActions()
    {
        _directionSwitcher.SetDirection(Input.GetAxis("Horizontal"));
        _mover.Move(_groundSpeed * _directionSwitcher.Direction);
    }

    protected override void ApplyJumpStateActions()
    {
        _jumper.Jump();
        _directionSwitcher.SetDirection(Input.GetAxis("Horizontal"));
        _mover.Move(_airHorizontalSpeed * Input.GetAxis("Horizontal"));
    }

    protected override void LoadConfig()
    {
        _groundSpeed = _config.GroundSpeed;
        _airHorizontalSpeed = _config.AirHorizontalSpeed;
        _jumpPower = _config.JumpPower;
        _reloadTime = _config.ReloadTime;
        CurrentState = _config.State;
        _projectile = _config.Projectile;
    }

    protected override void InitComponents()
    {
        _mover = GetComponent<Mover>();
        _jumper = GetComponent<Jumper>();
        _attacker = GetComponent<Attacker>();
        _collideDetector = GetComponent<CollideDetector>();
        _directionSwitcher = GetComponent<DirectionSwitcher>();

        _jumper.Init(GetComponent<Rigidbody2D>(), _jumpPower);
        _attacker.Init(_projectile, _reloadTime);
        _directionSwitcher.Init(_config.StartDirection);

        _collideDetector.PlatformCollided += _jumper.SetStatus;
    }
}

