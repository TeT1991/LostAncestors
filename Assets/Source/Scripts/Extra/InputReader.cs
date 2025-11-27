using System;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private readonly KeyCode _moveLeftButton = KeyCode.A;
    private readonly KeyCode _moveRightButton = KeyCode.D;
    private readonly KeyCode _jumpButton = KeyCode.Space;
    private readonly KeyCode _attackButton = KeyCode.Mouse0;

    private Dictionary<KeyCode, ButtonType> _keys;

    public event Action<ButtonType> ButtonPressed;
    public event Action<ButtonType> ButtonReleased;

    private void Update()
    {
        DetectButtonPress();
        DetectButtonRelease();
    }

    public void Init()
    {
        _keys = new()
        {
            { _moveRightButton, ButtonType.WalkRight },
            { _moveLeftButton, ButtonType.WalkLeft },
            { _jumpButton, ButtonType.Jump },
            { _attackButton, ButtonType.Attack },
        };
    }

    private void DetectButtonPress()
    {
        foreach (KeyCode key in _keys.Keys)
        {
            if (Input.GetKeyDown(key))
            {
                if (_keys.TryGetValue(key, out ButtonType buttonType))
                {
                    ButtonPressed?.Invoke(buttonType);
                }
            }
        }
    }

    private void DetectButtonRelease()
    {
        foreach (KeyCode key in _keys.Keys)
        {
            if (Input.GetKeyUp(key))
            {
                if (_keys.TryGetValue(key, out ButtonType buttonType))
                {
                    ButtonReleased?.Invoke(buttonType);
                }
            }
        }
    }
}
