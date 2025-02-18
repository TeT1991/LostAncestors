using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CharacterDetector : MonoBehaviour
{
    private Vector2 _direction;
    private Collider2D _collider;
    private float _distance = 3;
    private bool _isDetected;

    public bool IsDetected => _isDetected;

    private void Update()
    {
        TryDetectCharacter();
    }

    public void Init(float direction)
    {
        SetDirection(direction);
        _collider = GetComponent<Collider2D>();
    }

    public void SetDirection(float direction)
    {
        _direction = Vector2.right * direction;
    }

    private void TryDetectCharacter()
    {
        RaycastHit2D hit = Physics2D.Raycast(_collider.bounds.center, _direction.normalized, _distance);

        if (hit != false)
        {
            if (hit.collider.gameObject.TryGetComponent<Character>(out Character character))
            {
                _isDetected = true;
            }

            else
            {
                _isDetected = false;
            }
        }
    }
}
