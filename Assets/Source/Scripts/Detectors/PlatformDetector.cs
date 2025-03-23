using System;
using UnityEngine;

public class PlatformDetector : Detector
{
    public event Action<bool> Collided;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out GroundObstacle platform))
        {
            Collided?.Invoke(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out GroundObstacle platform))
        {
            Collided?.Invoke(false);
        }
    }
}