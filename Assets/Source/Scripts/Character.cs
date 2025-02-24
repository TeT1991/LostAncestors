using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Jumper), typeof(Attacker))]
[RequireComponent(typeof(CollideDetector), typeof(DirectionSwitcher), typeof(Rigidbody2D))]
public class Character : Entity
{
    private float _groundSpeed;
    private float _airHorizontalSpeed;
    private float _jumpHeight;
    private float _reloadTime;
    private Transform _projectile;

    private Mover _mover;
    private Jumper _jumper;
    private Attacker _attacker;
    private CollideDetector _collideDetector;
    private DirectionSwitcher _directionSwitcher;
    private CharacterStatesHandle _characterStatesSwitchCheck;

    private Rigidbody2D _rigidbody2D;

    public float GroundSpeed => _groundSpeed;
    public float AirHorizontalSpeed => _airHorizontalSpeed; 

    public Mover Mover => _mover;
    public Jumper Jumper => _jumper;
    public CollideDetector CollideDetector => _collideDetector;
    public DirectionSwitcher DirectionSwitcher => _directionSwitcher;

    public TMPro.TextMeshProUGUI _textMeshPro;

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
        _airHorizontalSpeed = _config.AirHorizontalSpeed;
        _jumpHeight = _config.JumpPower;
        _reloadTime = _config.ReloadTime;
        _projectile = _config.Projectile;
    }

    protected override void InitComponents()
    {
        base.InitComponents();

        _mover = GetComponent<Mover>();
        _jumper = GetComponent<Jumper>();
        _attacker = GetComponent<Attacker>();
        _collideDetector = GetComponent<CollideDetector>();
        _directionSwitcher = GetComponent<DirectionSwitcher>();
        _characterStatesSwitchCheck = GetComponent<CharacterStatesHandle>();
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _jumper.Init(_jumpHeight, _rigidbody2D);
        _attacker.Init(_projectile, _reloadTime);
        _directionSwitcher.Init(_config.StartDirection);
        _characterStatesSwitchCheck.Init(this);

        _directionSwitcher.DirectionChanged += FlipSprites;
        // _collideDetector.PlatformCollided += _jumper.SetStatus;
    }
}

