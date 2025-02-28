using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Attacker), typeof(Patroler))]
[RequireComponent(typeof(EnemyStatesHandle), typeof(DirectionSwitcher), typeof(EnemyCollideDetector))]
public class Enemy : Entity
{
    [SerializeField] private Transform _rayStartPoint;
    [SerializeField] private Transform _projectileLaunchPoint;

    private float _groundSpeed;
    private float _reloadTime;
    private Transform _projectile;

    private Mover _mover;
    private Attacker _attacker;
    private Patroler _patroler;
    private DirectionSwitcher _directionSwitcher;
    private EnemyCollideDetector _enemyCollideDetector;
    private EnemyStatesHandle _enemyStatesHandle;

    public float GroundSpeed => _groundSpeed;
    public Mover Mover => _mover;
    public Attacker Attacker => _attacker;
    public Transform Projectile => _projectile;
    public DirectionSwitcher DirectionSwitcher => _directionSwitcher;
    public EnemyCollideDetector EnemyCollideDetector => _enemyCollideDetector;
    public Transform ProjectileLaunchPoint => _projectileLaunchPoint;

    protected override void LoadConfig()
    {
        base.LoadConfig();

        _groundSpeed = Config.GroundSpeed;
        _reloadTime = Config.ReloadTime;
        _projectile = Config.RangeProjectile;
    }

    protected override void InitComponents()
    {
        base.InitComponents();

        _mover = GetComponent<Mover>();
        _attacker = GetComponent<Attacker>();
        _patroler = GetComponent<Patroler>();
        _enemyCollideDetector = GetComponent<EnemyCollideDetector>();
        _directionSwitcher = GetComponent<DirectionSwitcher>();
        _enemyStatesHandle = GetComponent<EnemyStatesHandle>();

        _attacker.Init(_reloadTime);
        _directionSwitcher.SetDirection(Config.StartDirection);
        _patroler.Init(_enemyCollideDetector, _directionSwitcher.Direction);
        _enemyCollideDetector.Init(_directionSwitcher.Direction, _rayStartPoint);
        _enemyStatesHandle.Init(this);

        _directionSwitcher.DirectionChanged += FlipSprites;

        _directionSwitcher.DirectionChanged += _enemyCollideDetector.SetDirection;
        _enemyCollideDetector.WallCollided += _directionSwitcher.ReverseDirection;

        _attacker.Reloaded += () => _enemyStatesHandle.SetAttackStatus(false);
    }
}
