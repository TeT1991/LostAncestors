using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyCollideDetector : MonoBehaviour
{
    private Collider2D _collider;
    private Transform _rayStartPoint;
    private Vector2 _direction;
    private float _viewDistance;
    private bool _isCharacterDetected;

    private string _characterLayerName;
    private string _platformLayerName;
    public bool IsCharacterDetected => _isCharacterDetected;

    public Action WallCollided;

    private void Update()
    {
        TryDetectCharacter();
        TryDetectObstacleCollision();
    }

    public void Init(float direction, Transform rayStartPoint)
    {
        _characterLayerName = "Characters";
        _platformLayerName = "Platform";

        _viewDistance = 10;
        _rayStartPoint = rayStartPoint;
        _collider = GetComponent<Collider2D>();
        SetDirection(direction);
    }

    public void SetDirection(float direction)
    {
        _direction = Vector2.right * direction;
    }

    private void TryDetectCharacter()
    {
        RaycastHit2D hit = Physics2D.Raycast(_rayStartPoint.position, _direction.normalized, _viewDistance, LayerMask.GetMask(_characterLayerName, _platformLayerName));

        if (hit != false && hit.collider.TryGetComponent<Character>(out Character character))
        {
            Debug.Log("t");
                _isCharacterDetected = true;
        }
        else
        {
            Debug.Log("f");
            _isCharacterDetected = false;
        }
    }

    private void TryDetectObstacleCollision()
    {
        float collideOffset = 0.1f;
        float rayStartXPoint = _collider.bounds.center.x + ((_collider.bounds.size.x / 2) * _direction.x);
        Vector2 rayStartPoint = new(rayStartXPoint, _collider.bounds.center.y);

        RaycastHit2D hit = Physics2D.Raycast(rayStartPoint, _direction, collideOffset, LayerMask.GetMask(_platformLayerName));

        if(hit != false)
        {
            WallCollided?.Invoke();
        }
    }
}
