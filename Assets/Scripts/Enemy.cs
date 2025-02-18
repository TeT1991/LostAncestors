using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Attacker), typeof(Patroler))]
[RequireComponent(typeof(CollideDetector), typeof(DirectionSwitcher), typeof(CharacterDetector))]
public class Enemy : Entity
{
    private float _groundSpeed;
    private float _reloadTime;
    private EntityStates _currentState;
    private Transform _projectile;

    private Mover _mover;
    private Attacker _attacker;
    private Patroler _patroler;
    private CollideDetector _collideDetector;
    private DirectionSwitcher _directionSwitcher;
    private CharacterDetector _characterDetector;

    protected override void Init()
    {
        base.Init();

        Conditions = new List<StateConditions>
        {
            new EnemyRangeAttackConditions(_attacker,_characterDetector),
            new PatrolingStateConditions(_characterDetector, _attacker),
        };
    }

    protected override void Update()
    {
        base.Update();
        _textMeshPro.text = _currentState.ToString();
    }

    protected override void ApplyStateActions()
    {
        switch (_currentState)
        {
            case EntityStates.Patroling:
                ApplyPatrolingStateActions();
                break;

            case EntityStates.RangeAttack:
                ApplyRangeAttackStateActions();
                break;
        }
    }

    protected override void ApplyRangeAttackStateActions()
    {
        _attacker.ApplyRangeAttack(_directionSwitcher.Direction);
    }

    protected override void ApplyPatrolingStateActions()
    {
        _mover.Move(_groundSpeed * _directionSwitcher.Direction);
    }

    protected override void LoadConfig()
    {
        _groundSpeed = _config.GroundSpeed;
        _reloadTime = _config.ReloadTime;
        _currentState = _config.State;
        _projectile = _config.Projectile;
    }

    protected override void InitComponents()
    {
        _mover = GetComponent<Mover>();
        _attacker = GetComponent<Attacker>();
        _patroler = GetComponent<Patroler>();
        _characterDetector = GetComponent<CharacterDetector>();
        _collideDetector = GetComponent<CollideDetector>();

        _attacker.Init(_projectile, _reloadTime);
        _directionSwitcher.SetDirection(_config.StartDirection);
        _patroler.Init(_collideDetector, _directionSwitcher.Direction);
        _characterDetector.Init(_directionSwitcher.Direction);

        _collideDetector.ObstacleCollided += _directionSwitcher.ReverseDirection;
        _directionSwitcher.DirectionChanged += FlipSprites;
        _directionSwitcher.DirectionChanged += _characterDetector.SetDirection;
    }
}
