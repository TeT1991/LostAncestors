using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LadderDetector : Detector
{
    public event Action<bool> Collided;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Ladder ladder))
        {
            Collided?.Invoke(true);
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Ladder ladder))
        {
            Collided?.Invoke(false);
        }
    }
}
