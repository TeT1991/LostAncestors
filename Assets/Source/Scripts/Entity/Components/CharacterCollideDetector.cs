using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CharacterCollideDetector : MonoBehaviour
{
    private string _projectileLayerName;
    private string _platformLayerName;
    private string _pickablesLayerName;

    private Collider2D _collider;

    private bool _isPlatformCollided;

    public Action<bool> PlatformCollided;
    public Action ObstacleCollided;
    public Action<int, OwnerType> ProjectileCollided;
    public Action<Pickable> PickaleCollided;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _projectileLayerName = "Projectiles";
        _platformLayerName = "Platform";
        _pickablesLayerName = "Pickables";
    }

    private void Update()
    {
        DetectPlatformCollision();
        DetectProjectileCollision();
        DetectPickableCollision();
    }

    private void DetectPlatformCollision()
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

    private void DetectProjectileCollision()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(_collider.bounds.center, _collider.bounds.size, 0, LayerMask.GetMask(_projectileLayerName));

        if (hits.Length > 0)
        {
            foreach (var hit in hits)
            {
                if (hit.TryGetComponent<Projectile>(out Projectile projectile))
                {
                    ProjectileCollided?.Invoke(projectile.Damage, projectile.OwnerType);
                }
            }
        }
    }

    private void DetectPickableCollision()
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
