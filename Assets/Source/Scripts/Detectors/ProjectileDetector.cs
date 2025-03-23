using System;
using UnityEngine;

public class ProjectileDetector : Detector
{
    public event Action<Projectile> Collided;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Projectile projectile))
        {
            Collided(projectile);
        }
    }
}
