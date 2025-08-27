using System;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    public event Action OnGroundDetected;
    public event Action OnGroundNotDetected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Platform>(out _))
        {
            OnGroundDetected?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Platform>(out _))
        {
            OnGroundNotDetected?.Invoke();
        }
    }
}
