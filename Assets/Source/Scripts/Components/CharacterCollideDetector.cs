using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CharacterCollideDetector : MonoBehaviour
{
    [SerializeField] private PlatformDetector _platformDetector;
    [SerializeField] private WallDetector _wallDetector;
    [SerializeField] private InteractableDetector _interactableDetector;

    private string _projectileLayerName;
    private string _platformLayerName;
    private string _pickablesLayerName;

    private Vector2 _direction;

    private Collider2D _collider;
    private DirectionSwitcher _directionSwitcher;

    private bool _isPlatformCollided;
    private bool _isWallCollided;

    public bool IsWallCollided => _isWallCollided;

    public Action<bool> PlatformCollided;
    public Action ObstacleCollided;
    public Action<int> ProjectileCollided;
    public Action<Pickable> PickaleCollided;

    public event Action<IInteractable> InteractableCollided;

    private void Update()
    {
        //TryDetectGroundCollision();
        //TryDetectWallCollision();
        TryDetectProjectileCollision();
        TryDetectPickableCollision();
    }

    public void Init(DirectionSwitcher directionSwitcher, float direction)
    {
        _collider = GetComponent<Collider2D>();
        _directionSwitcher = directionSwitcher;
        _projectileLayerName = "Projectiles";
        _platformLayerName = "Platform";
        _pickablesLayerName = "Pickables";


        _platformDetector.Init(_collider);
        _wallDetector.Init();
        _interactableDetector.Init();

        SetDirection(direction);

        _platformDetector.Collided += HasPlatformCollided;
        _wallDetector.Collided += HasDetectWallCollision;
        _interactableDetector.Collided += HasInteractableCollided;
    }

    public void SetDirection(float direction)
    {
        _direction = (Vector2.right * direction).normalized;
        _wallDetector.FlipColliderDirection(_direction.x);
    }

    private void HasPlatformCollided(bool value)
    {
        _isPlatformCollided = value;
        PlatformCollided?.Invoke(value);
    }

    private void HasDetectWallCollision(bool value)
    {
        _isWallCollided = value;
    }

    private void HasInteractableCollided(IInteractable interactable)
    {
        InteractableCollided?.Invoke(interactable);
    }

    private void TryDetectProjectileCollision()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(_collider.bounds.center, _collider.bounds.size, 0, LayerMask.GetMask(_projectileLayerName));

        if (hits.Length > 0)
        {
            foreach (var hit in hits)
            {
                if (hit.TryGetComponent(out EnemyProjectile projectile))
                {
                    ProjectileCollided?.Invoke(projectile.Damage);
                    projectile.Destroy();

                }
            }
        }
    }

    private void TryDetectPickableCollision()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(_collider.bounds.center, _collider.bounds.size, 0, LayerMask.GetMask(_pickablesLayerName));

        if (hits.Length > 0)
        {
            foreach (var hit in hits)
            {
                if (hit.TryGetComponent<Pickable>(out Pickable pickable))

                {
                    PickaleCollided.Invoke(pickable);
                }
            }
        }
    }
}
