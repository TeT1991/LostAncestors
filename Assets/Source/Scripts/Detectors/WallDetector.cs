using System;
using UnityEngine;

public class WallDetector : Detector
{
    public event Action<bool> Collided;

    public void FlipColliderDirection(float direction)
    {
        float rightOffset = 0;
        float leftOffset = -0.5f;
        float currentOffset = direction > 0 ? rightOffset : leftOffset;

        Collider.offset = new Vector2(currentOffset, 0);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Obstacle platform))
        {
            Collided?.Invoke(true);
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Obstacle platform))
        {
            Collided?.Invoke(false);
        }
    }
}
