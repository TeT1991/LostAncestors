using System;
using UnityEngine;

public class Mover
{
    private readonly Rigidbody2D _rigidbody2D;
    private int _direction;
    private float _speed;

    public event Action<int> OnDirecctionChanged;

    public Mover(Rigidbody2D rigidboy, float speed)
    {
        _rigidbody2D = rigidboy;
        _speed = speed;
    }

    public void Move()
    {
        float deltaX = _speed * _direction;

        _rigidbody2D.velocity = new Vector2(deltaX, _rigidbody2D.velocity.y);
    }

    public void SetDirection(int direction)
    {
        _direction = direction;
        OnDirecctionChanged?.Invoke(_direction);
    }
}
