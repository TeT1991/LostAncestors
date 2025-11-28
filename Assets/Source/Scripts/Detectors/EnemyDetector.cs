using System;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [SerializeField] private CircleCollider2D _circleCollider2D;

    public event Action<Enemy> Detected;
    public event Action<Enemy> NotDetected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Enemy>(out  Enemy enemy))
        {
            Detected?.Invoke(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            NotDetected?.Invoke(enemy);
        }
    }
}
