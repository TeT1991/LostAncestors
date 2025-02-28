using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Jumper), typeof(Attacker))]
[RequireComponent(typeof(CharacterCollideDetector), typeof(DirectionSwitcher))]
public class Character : Entity, IDamagable
{
    [SerializeField] private Transform _projectileRangeLaunchPoint;
    [SerializeField] private Transform _projectileMeleeLaunchPoint;

    private float _groundSpeed;
    private float _airHorizontalSpeed;
    private float _jumpHeight;
    private float _reloadTime;
    private Transform _rangeProjectile;
    private Transform _meleeProjectile;

    private Mover _mover;
    private Jumper _jumper;
    private Attacker _attacker;
    private CharacterCollideDetector _collideDetector;
    private DirectionSwitcher _directionSwitcher;
    private CharacterStatesHandle _characterStatesSwitchCheck;

    private Rigidbody2D _rigidbody2D;

    public float GroundSpeed => _groundSpeed;
    public float AirHorizontalSpeed => _airHorizontalSpeed;
    public Transform RangeProjectile => _rangeProjectile;
    public Transform MeleeProjectile => _meleeProjectile;
    public Transform ProjectileRangeLaunchPoint => _projectileRangeLaunchPoint;
    public Transform ProjectileMeleeLaunchPoint => _projectileMeleeLaunchPoint;

    public Mover Mover => _mover;
    public Jumper Jumper => _jumper;
    public Attacker Attacker => _attacker;
    public CharacterCollideDetector CollideDetector => _collideDetector;
    public DirectionSwitcher DirectionSwitcher => _directionSwitcher;

    public TMPro.TextMeshProUGUI _textMeshPro;

    protected override void LoadConfig()
    {
        base.LoadConfig();

        _groundSpeed = Config.GroundSpeed;
        _airHorizontalSpeed = Config.AirHorizontalSpeed;
        _jumpHeight = Config.JumpPower;
        _reloadTime = Config.ReloadTime;
        _rangeProjectile = Config.RangeProjectile;
        _meleeProjectile = Config.MeleeProjectile;
    }

    protected override void InitComponents()
    {
        base.InitComponents();

        _mover = GetComponent<Mover>();
        _jumper = GetComponent<Jumper>();
        _attacker = GetComponent<Attacker>();
        _collideDetector = GetComponent<CharacterCollideDetector>();
        _directionSwitcher = GetComponent<DirectionSwitcher>();
        _characterStatesSwitchCheck = GetComponent<CharacterStatesHandle>();
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _jumper.Init(_jumpHeight);
        _attacker.Init(_reloadTime);
        _directionSwitcher.Init(Config.StartDirection);
        _characterStatesSwitchCheck.Init(this);

        _collideDetector.PlatformCollided += _characterStatesSwitchCheck.SetJumpingStatus;
        _collideDetector.ProjectileCollided += TryApplyDamage;
        _collideDetector.PickaleCollided += TryPickUp;

        _directionSwitcher.DirectionChanged += FlipSprites;
        _attacker.Reloaded += () => _characterStatesSwitchCheck.SetAttackStatus(false);

        OwnerType = OwnerType.Character;
    }

    public void TryApplyDamage(int value, OwnerType ownerType)
    {
        if (OwnerType != ownerType)
        {
            HealthHandler.ApplyDamage(value);
        }
    }

    public void TryPickUp(Pickable pickable)
    {
        if(pickable.PickableType == PickableType.Health)
        {
            if(HealthHandler.Health < HealthHandler.MaxHealth)
            {
                int healCount = 1;
                HealthHandler.ApplyHeal(healCount);
                pickable.PickUp();
            }
        }
    }
}

