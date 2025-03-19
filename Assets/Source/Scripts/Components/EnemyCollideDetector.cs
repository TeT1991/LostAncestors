using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
public class EnemyCollideDetector : MonoBehaviour
{
    private Collider2D _collider;
    private Transform _rayStartPoint;

    private Vector2 _direction;
    private float _viewDistance;
    private bool _isCharacterDetected;
    private bool _isWallCollided;

    private string _characterLayerName;
    private string _platformLayerName;
    private string _projectileLayerName;
    public bool IsCharacterDetected => _isCharacterDetected;

    public Action WallCollided;
    public Action<int> ProjectileCollided;

    private Button _button;

    private void Update()
    {
        TryDetectCharacter();
        TryDetectObstacleCollision();
        TryDetectProjectileCollision();
    }

    public void Init(float direction, Transform rayStartPoint)
    {
        _characterLayerName = "Characters";
        _platformLayerName = "Platform";
        _projectileLayerName = "Projectiles";

        _viewDistance = 10;
        _rayStartPoint = rayStartPoint;
        _isWallCollided = false;
        _collider = GetComponent<Collider2D>();
        SetDirection(direction);
    }

    public void SetDirection(float direction)
    {
        _direction = Vector2.right * direction;
    }

    private void TryDetectCharacter()
    {
        RaycastHit2D hit = Physics2D.Raycast(_rayStartPoint.position, _direction.normalized, _viewDistance, LayerMask.GetMask("Through"));

        if (hit != false && hit.collider.TryGetComponent(out Character character))
        {
            _isCharacterDetected = true;
        }
        else
        {
            _isCharacterDetected = false;
        }
    }

    private void TryDetectProjectileCollision()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(_collider.bounds.center, _collider.bounds.size, 0, LayerMask.GetMask(_projectileLayerName));

        if (hits.Length > 0)
        {
            foreach (var hit in hits)
            {
                if (hit.TryGetComponent(out PlayerProjectile projectile))
                {
                        ProjectileCollided?.Invoke(projectile.Damage);
                        projectile.Destroy();
                }
            }
        }
    }

    private void TryDetectObstacleCollision()
    {
        float detectionOffset = _collider.bounds.extents.x + 0.1f;
        RaycastHit2D hit = Physics2D.Raycast(_collider.bounds.center, _direction.normalized, detectionOffset, LayerMask.GetMask(_platformLayerName));

        Debug.DrawRay(_collider.bounds.center, _direction.normalized * detectionOffset, Color.red, Time.deltaTime);

        if (hit)
        {
            WallCollided?.Invoke();
        }
    }
}
