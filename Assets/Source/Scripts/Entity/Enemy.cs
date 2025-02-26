using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Attacker), typeof(Patroler))]
[RequireComponent(typeof(CollideDetector), typeof(DirectionSwitcher), typeof(CharacterDetector))]
[RequireComponent(typeof(EnemyStatesHandle))]
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
    private CollideDetector _collideDetector;
    private DirectionSwitcher _directionSwitcher;
    private CharacterDetector _characterDetector;
    private EnemyStatesHandle _enemyStatesHandle;

    public float GroundSpeed => _groundSpeed;

    public Mover Mover => _mover;
    public Attacker Attacker => _attacker;
    public Transform Projectile => _projectile;
    public CollideDetector CollideDetector => _collideDetector;
    public DirectionSwitcher DirectionSwitcher => _directionSwitcher;
    public CharacterDetector CharacterDetector => _characterDetector;
    public Transform ProjectileLaunchPoint => _projectileLaunchPoint;

    protected override void LoadConfig()
    {
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
        _characterDetector = GetComponent<CharacterDetector>();
        _collideDetector = GetComponent<CollideDetector>();
        _directionSwitcher = GetComponent<DirectionSwitcher>();
        _enemyStatesHandle = GetComponent<EnemyStatesHandle>();

        _attacker.Init(_reloadTime);
        _directionSwitcher.SetDirection(Config.StartDirection);
        _patroler.Init(_collideDetector, _directionSwitcher.Direction);
        _characterDetector.Init(_directionSwitcher.Direction, _rayStartPoint);
        _enemyStatesHandle.Init(this);

        _collideDetector.ObstacleCollided += _directionSwitcher.ReverseDirection;
        _directionSwitcher.DirectionChanged += FlipSprites;
        _directionSwitcher.DirectionChanged += _characterDetector.SetDirection;
        _attacker.Reloaded += () => _enemyStatesHandle.SetAttackStatus(false);
    }
}
