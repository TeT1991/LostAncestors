using System;
using UnityEngine;

public class Mover
{
    private readonly Rigidbody2D _rigidbody2D;
    private int _direction;
    private bool _canMove;

    public event Action<int> OnDirecctionChanged;

    public Mover(Rigidbody2D rigidboy)
    {
        _rigidbody2D = rigidboy;
        DenyMove();
    }

    public void Move(float speed)
    {
        if (_canMove)
        {
            float deltaX = speed * _direction;

            _rigidbody2D.velocity = new Vector2(deltaX, _rigidbody2D.velocity.y);
        }
        else
        {
            _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
        }
    }

    public void SetDirection(int direction)
    {
        _direction = direction;
        OnDirecctionChanged?.Invoke(_direction);
    }

    public void AllowMove()
    {
        _canMove = true;
    }

    public void DenyMove()
    {
        _canMove = false;
    }
}
