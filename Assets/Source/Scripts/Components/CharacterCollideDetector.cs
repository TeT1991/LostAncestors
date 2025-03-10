using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CharacterCollideDetector : MonoBehaviour
{
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

    private void Update()
    {
        TryDetectGroundCollision();
        TryDetectWallCollision();
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

        SetDirection(direction);
    }

    public void SetDirection(float direction)
    {
        _direction = (Vector2.right * direction).normalized;
    }

    private void TryDetectGroundCollision()
    {
        float detectionOffset = 0.1f;
        Collider2D[] hits = Physics2D.OverlapPointAll(_collider.bounds.min - new Vector3(0, detectionOffset), LayerMask.GetMask(_platformLayerName)); ;
        bool isPlatformCollided = false;

        if (hits.Length > 0)
        {
            foreach (var hit in hits)
            {
                if (hit.TryGetComponent<Platform>(out Platform platform))
                {
                    isPlatformCollided = true;
                }
                else
                {
                    isPlatformCollided = false;
                }
            }
        }

        if (_isPlatformCollided != isPlatformCollided)
        {
            _isPlatformCollided = isPlatformCollided;
            PlatformCollided?.Invoke(_isPlatformCollided);
        }
    }

    private void TryDetectWallCollision()
    {
        float detectionOffset = _collider.bounds.extents.x + 0.1f;

        RaycastHit2D hit = Physics2D.Raycast(_collider.bounds.center, _direction.normalized, detectionOffset, LayerMask.GetMask(_platformLayerName));

        Debug.DrawRay(_collider.bounds.center, _direction.normalized * detectionOffset, Color.red, Time.deltaTime);

        if (hit)
        {
            _isWallCollided = true;
        }
        else
        {
            _isWallCollided = false;
        }
    }

    private void TryDetectProjectileCollision()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(_collider.bounds.center, _collider.bounds.size, 0, LayerMask.GetMask(_projectileLayerName));

        if (hits.Length > 0)
        {
            foreach (var hit in hits)
            {
                if (hit.TryGetComponent<Projectile>(out Projectile projectile))
                {
                    if (projectile.OwnerType != OwnerType.Character)
                    {
                        ProjectileCollided?.Invoke(projectile.Damage);
                        projectile.Destroy();
                    }
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
