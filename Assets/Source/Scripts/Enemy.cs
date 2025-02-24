using Spine.Unity;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Attacker), typeof(Patroler))]
[RequireComponent(typeof(CollideDetector), typeof(DirectionSwitcher), typeof(CharacterDetector))]
[RequireComponent(typeof(AnimationSwitcher))]
public class Enemy : Entity
{
    private float _groundSpeed;
    private float _reloadTime;
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
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void LoadConfig()
    {
        _groundSpeed = _config.GroundSpeed;
        _reloadTime = _config.ReloadTime;
        _projectile = _config.Projectile;
    }

    protected override void InitComponents()
    {
        _mover = GetComponent<Mover>();
        _attacker = GetComponent<Attacker>();
        _patroler = GetComponent<Patroler>();
        _characterDetector = GetComponent<CharacterDetector>();
        _collideDetector = GetComponent<CollideDetector>();
        _directionSwitcher = GetComponent<DirectionSwitcher>();

        _attacker.Init(_projectile, _reloadTime);
        _directionSwitcher.SetDirection(_config.StartDirection);
        _patroler.Init(_collideDetector, _directionSwitcher.Direction);
        _characterDetector.Init(_directionSwitcher.Direction);

        _collideDetector.ObstacleCollided += _directionSwitcher.ReverseDirection;
        _directionSwitcher.DirectionChanged += FlipSprites;
        _directionSwitcher.DirectionChanged += _characterDetector.SetDirection;
    }
}
