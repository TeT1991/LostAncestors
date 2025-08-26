using System;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    public event Action<bool> OnGroundDetected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Platform>(out Platform platform))
        {
            OnGroundDetected?.Invoke(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Platform>(out Platform platform))
        {
            OnGroundDetected?.Invoke(false);
        }
    }
}
