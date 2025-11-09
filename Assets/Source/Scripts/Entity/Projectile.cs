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
