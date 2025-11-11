using UnityEngine;

[RequireComponent (typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    private float _speed;
    private float _direction;
    private float _moveDistance;

    private void Update()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamagable damagable))
        {
            damagable.TakeDamage();
            Destroy(gameObject);
        }

        if (collision.TryGetComponent<Platform>(out _))
        {
            Destroy(gameObject);
        }
    }

    public void Init(float speed, float direction)
    {
        _speed = speed;
        _direction = direction;
        _moveDistance = _direction * _speed * Time.deltaTime;
    }

    private void Move()
    {
        transform.position += Vector3.right * _moveDistance;
    }
}
