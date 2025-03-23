using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Jumper))]
[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(CharacterStatesHandle))]
[RequireComponent(typeof(CharacterCollideDetector))]
[RequireComponent(typeof(Rigidbody2D))]
public class Character : Entity, IDamagable
{
    [SerializeField] private Transform _projectileRangeLaunchPoint;
    [SerializeField] private Transform _projectileMeleeLaunchPoint;

    private float _groundSpeed;
    private float _airHorizontalSpeed;
    private float _jumpHeight;
    private float _reloadTime;
    private Projectile _rangeProjectile;
    private Projectile _meleeProjectile;

    private Mover _mover;
    private Jumper _jumper;
    private Attacker _attacker;
    private CharacterCollideDetector _collideDetector;
    private DirectionSwitcher _directionSwitcher;
    private CharacterStatesHandle _characterStatesSwitchCheck;

    private Rigidbody2D _rigidBody;

    private IInteractable _currentInteractable;

    public float GroundSpeed => _groundSpeed;
    public float AirHorizontalSpeed => _airHorizontalSpeed;
    public Projectile RangeProjectile => _rangeProjectile;
    public Projectile MeleeProjectile => _meleeProjectile;
    public Transform ProjectileRangeLaunchPoint => _projectileRangeLaunchPoint;
    public Transform ProjectileMeleeLaunchPoint => _projectileMeleeLaunchPoint;

    public Mover Mover => _mover;
    public Jumper Jumper => _jumper;
    public Attacker Attacker => _attacker;
    public CharacterCollideDetector CollideDetector => _collideDetector;
    public DirectionSwitcher DirectionSwitcher => _directionSwitcher;
    public Rigidbody2D RigidBody => _rigidBody;

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
        _characterStatesSwitchCheck = GetComponent<CharacterStatesHandle>();
        _rigidBody = GetComponent<Rigidbody2D>();

        _directionSwitcher = new DirectionSwitcher();

        _jumper.Init(_jumpHeight, _rigidBody);
        _attacker.Init(_reloadTime);
        _directionSwitcher.Init(Config.StartDirection);
        _collideDetector.Init(_directionSwitcher, _directionSwitcher.Direction);
        _characterStatesSwitchCheck.Init(this);

        _collideDetector.PlatformCollided += _characterStatesSwitchCheck.SetJumpingStatus;
        _collideDetector.LadderColided += _characterStatesSwitchCheck.SetClimbingStatus;
        _collideDetector.ProjectileCollided += ApplyDamage;
        _collideDetector.PickaleCollided += TryPickUp;
        _collideDetector.InteractableCollided += SetCurrentInteractable;

        _directionSwitcher.DirectionChanged += FlipSprites;
        _directionSwitcher.DirectionChanged += _collideDetector.SetDirection;
        _attacker.Reloaded += () => _characterStatesSwitchCheck.SetAttackStatus(false);

        OwnerType = OwnerType.Character;
    }

    public void ApplyDamage(int value)
    {
        HealthHandler.ApplyDamage(value);
    }

    public void TryPickUp(Pickable pickable)
    {
        if (pickable.PickableType == PickableType.Health)
        {
            if (HealthHandler.Health < HealthHandler.MaxHealth)
            {
                int healCount = 1;
                HealthHandler.ApplyHeal(healCount);
                pickable.PickUp();
            }
        }
    }

    private void Update()
    {
        TryInteract();
    }

    private void TryInteract()
    {
        if (_currentInteractable is InteractableButton interactable)
        {
            if (Input.GetKey(KeyCode.E))
            {
                _currentInteractable.Interact();
            }
        }

        switch (_currentInteractable)
        {
            case InteractableButton interactableButton:

                if (Input.GetKey(KeyCode.E))
                {
                    _currentInteractable.Interact();
                }
                break;

            case MovingPlatform movingPlatform:

                if (Input.GetKeyDown(KeyCode.W))
                {
                    movingPlatform.SetNextStop(1);
                    _currentInteractable.Interact();
                }

                if (Input.GetKeyDown(KeyCode.S))
                {
                    movingPlatform.SetNextStop(-1);
                    _currentInteractable.Interact();
                }
                break;
        }
    }

    private void SetCurrentInteractable(IInteractable interactable)
    {
        _currentInteractable = interactable;
    }

    private void ResetCurrentInterractable()
    {
        _currentInteractable = null;
    }

    private void OnDestroy()
    {
        _collideDetector.PlatformCollided -= _characterStatesSwitchCheck.SetJumpingStatus;
        _collideDetector.ProjectileCollided -= ApplyDamage;
        _collideDetector.PickaleCollided -= TryPickUp;

        _directionSwitcher.DirectionChanged -= FlipSprites;
        _directionSwitcher.DirectionChanged -= _collideDetector.SetDirection;
        _attacker.Reloaded -= () => _characterStatesSwitchCheck.SetAttackStatus(false);
    }
}

