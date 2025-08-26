using System;
using UnityEngine;

public class EntityMover
{
    private readonly Entity _entity;
    private int _direction;
    private bool _canMove;

    public event Action<int> OnDirecctionChanged;

    public EntityMover(Entity entity)
    {
        _entity = entity;
        DenyMove();
    }

    public void Move(float speed)
    {
        if (_canMove)
        {
            float deltaX = speed * Time.deltaTime * _direction;
            float newXPosition = _entity.transform.position.x + deltaX;
            _entity.transform.position = new Vector3(newXPosition,
                _entity.transform.position.y, _entity.transform.position.z);
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
