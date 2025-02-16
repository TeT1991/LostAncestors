using System;
using UnityEngine;

public class CollideDetector : MonoBehaviour
{
    private Collider2D _collider;

    public Action<bool> PlatformCollided;
    public Action ObstacleCollided;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Platform>(out Platform platform)){
            PlatformCollided?.Invoke(true);
        }

        if(collision.gameObject.TryGetComponent<Obstacle>(out Obstacle obstacle)){
            ObstacleCollided?.Invoke();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        PlatformCollided?.Invoke(false);
    }

}
