using System;
using UnityEngine;

public class DirectionSwitcher : MonoBehaviour
{
    private float _direction;

    public float Direction => _direction;

    public Action<float> DirectionChanged;

    public void Init(float direction)
    {
        SetDirection(direction);
    }

    public void SetDirection(float direction)
    {
        if (direction != 0)
        {
            DirectionChanged?.Invoke(_direction);
            _direction = direction;
        }
    }

    public void ReverseDirection()
    {
        _direction = -_direction;
        DirectionChanged?.Invoke(_direction);
    }
}

