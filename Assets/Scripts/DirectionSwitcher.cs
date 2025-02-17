using System;
using UnityEngine;

public class DirectionSwitcher : MonoBehaviour
{
    private float _direction = 1;

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
            _direction = direction;
            DirectionChanged?.Invoke(_direction);
        }
    }

    public void ReverseDirection()
    {
        float directionValue = -1;
        _direction *= directionValue;
        DirectionChanged?.Invoke(_direction);
    }
}

