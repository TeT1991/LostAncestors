using System;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    private int _collisionsCount = 0;

    public event Action OnGroundDetected;
    public event Action OnGroundNotDetected;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Platform>(out _))
        {
            _collisionsCount++;
            OnGroundDetected?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Platform>(out _))
        {
            _collisionsCount--;

            if (_collisionsCount <= 0)
            {
                _collisionsCount = 0;
                OnGroundNotDetected?.Invoke();
            }
        }
    }
}
