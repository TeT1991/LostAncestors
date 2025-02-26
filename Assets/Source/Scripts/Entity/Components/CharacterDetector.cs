using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CharacterDetector : MonoBehaviour
{
    private Transform _rayStartPoint;
    private Vector2 _direction;
    private float _distance;
    private bool _isDetected;

    public bool IsDetected => _isDetected;

    private void Update()
    {
        TryDetectCharacter();
    }

    public void Init(float direction, Transform rayStartPoint)
    {
        _distance = 10;
        _rayStartPoint = rayStartPoint;
        SetDirection(direction);
    }

    public void SetDirection(float direction)
    {
        _direction = Vector2.right * direction;
    }

    private void TryDetectCharacter()
    {
        RaycastHit2D hit = Physics2D.Raycast(_rayStartPoint.position, _direction.normalized, _distance);
        Debug.DrawRay(_rayStartPoint.position, _direction.normalized * _distance, Color.yellow, Time.deltaTime);

        if (hit != false)
        {
            if (hit.collider.gameObject.TryGetComponent<Character>(out Character character))
            {
                _isDetected = true;
            }
        }
        else
        {
            _isDetected = false;
        }
    }
}
