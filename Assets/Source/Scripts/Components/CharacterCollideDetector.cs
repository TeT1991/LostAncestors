using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CharacterCollideDetector : MonoBehaviour
{
    [SerializeField] private PlatformDetector _platformDetector;
    [SerializeField] private WallDetector _wallDetector;
    [SerializeField] private InteractableDetector _interactableDetector;
    [SerializeField] private ProjectileDetector _projectileDetector;
    [SerializeField] private LadderDetector _ladderDetector;

    private string _platformLayerName;
    private string _pickablesLayerName;

    private Vector2 _direction;

    private Collider2D _collider;
    private DirectionSwitcher _directionSwitcher;

    private bool _isGroundCollided;
    private bool _isWallCollided;
    private bool _isLadderCollided;

    public bool IsGroundCollided => _isGroundCollided;
    public bool IsWallCollided => _isWallCollided;
    public bool IsLadderColided => _isLadderCollided;

    public Action<bool> PlatformCollided;
    public Action ObstacleCollided;
    public Action<int> ProjectileCollided;
    public Action<Item> PickaleCollided;
    public Action<bool> LadderColided;

    public event Action<IInteractable> InteractableCollided;

    private void Update()
    {
        TryDetectPickableCollision();
    }

    public void Init(DirectionSwitcher directionSwitcher, float direction)
    {
        _collider = GetComponent<Collider2D>();
        _directionSwitcher = directionSwitcher;

        _platformDetector.Init();
        _wallDetector.Init();
        _interactableDetector.Init();
        _projectileDetector.Init(_collider);
        _ladderDetector.Init();

        SetDirection(direction);

        _platformDetector.Collided += HasDetectPlatformCollision;
        _wallDetector.Collided += HasDetectWallCollision;
        _interactableDetector.Colided += HasDetectInteractableCollision;
        _projectileDetector.Collided += HasDetectProjectileCollision;
        _ladderDetector.Collided += HadDetectLadderCollision;
    }

    public void SetDirection(float direction)
    {
        _direction = (Vector2.right * direction).normalized;
        _wallDetector.FlipColliderDirection(_direction.x);
    }

    private void HasDetectPlatformCollision(bool value)
    {
        _isGroundCollided = value;
        PlatformCollided?.Invoke(value);
    }

    private void HasDetectWallCollision(bool value)
    {
        _isWallCollided = value;
    }

    private void HasDetectInteractableCollision(IInteractable interactable)
    {
        InteractableCollided?.Invoke(interactable);
    }

    private void HasDetectProjectileCollision(Projectile projectile)
    {
        if(projectile is EnemyProjectile)
        {
            ProjectileCollided?.Invoke(projectile.Damage);
        }
    }

    private void HadDetectLadderCollision(bool value)
    {
        _isLadderCollided = value;
        LadderColided?.Invoke(value);   
    }

    private void TryDetectPickableCollision()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(_collider.bounds.center, _collider.bounds.size, 0, LayerMask.GetMask(_pickablesLayerName));

        if (hits.Length > 0)
        {
            foreach (var hit in hits)
            {
                if (hit.TryGetComponent<Item>(out Item pickable))

                {
                    PickaleCollided.Invoke(pickable);
                }
            }
        }
    }
}
