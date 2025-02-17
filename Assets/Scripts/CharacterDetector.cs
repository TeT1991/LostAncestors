using System;
using UnityEngine;

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
        Debug.DrawRay(_collider.bounds.center, _direction.normalized * _distance, Color.red, Time.deltaTime, false);
        int indexTocheck = 1;
        RaycastHit2D[] hits = Physics2D.RaycastAll(_collider.bounds.center, _direction.normalized, _distance, ~0);

        if (hits.Length > indexTocheck)
        {
            if (hits[indexTocheck].collider.gameObject.TryGetComponent<Character>(out Character character))
            {
                _isDetected = true;
            }
            else
            {
                _isDetected = false;
            }
        }
        else
        {
            _isDetected = false;
        }
    }
}
