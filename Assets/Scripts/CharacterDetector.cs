using System;
using UnityEngine;

public class CharacterDetector : MonoBehaviour
{
    private Vector2 _direction;
    private Collider2D _collider;
    private float _distance = 3;

    public Action PlayerDetected;

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
        Debug.DrawRay(_collider.bounds.center, _direction * _distance, Color.red, Time.deltaTime, false);
        RaycastHit2D hit = Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, transform.eulerAngles.z, _direction, _distance);

        if (hit != false)
        {
            if (hit.collider.gameObject.TryGetComponent<Character>(out Character character))
            {
                Debug.Log("Detected");
                PlayerDetected?.Invoke();
            }
        }
    }
}
