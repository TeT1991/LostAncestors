using System;
using System.Collections.Generic;
using UnityEngine;

public class InputReader:MonoBehaviour
{
    private readonly KeyCode _moveLeftButton = KeyCode.A;
    private readonly KeyCode _moveRightButton = KeyCode.D;
    private readonly KeyCode _jumpButton = KeyCode.Space;

    private readonly Dictionary<KeyCode, ButtonType> _keys;

    public event Action<ButtonType> OnButtonPressed;
    public event Action<ButtonType> OnButtonReleased;

    public InputReader()
    {
        _keys = new()
        {
            { _moveRightButton, ButtonType.Walk_right },
            { _moveLeftButton, ButtonType.Walk_left },
            { _jumpButton, ButtonType.Jump }
        };
    }

    private void Update()
    {
        DetectButtonPress();
        DetectButtonRelease();
    }

    private void DetectButtonPress()
    {
        foreach (KeyCode key in _keys.Keys)
        {
            if (Input.GetKeyDown(key))
            {
                _keys.TryGetValue(key, out ButtonType buttonType);
                OnButtonPressed?.Invoke(buttonType);
            }
        }
    }

    private void DetectButtonRelease()
    {
        foreach (KeyCode key in _keys.Keys)
        {
            if (Input.GetKeyUp(key))
            {
                _keys.TryGetValue(key, out ButtonType buttonType);
                OnButtonReleased?.Invoke(buttonType);
            }
        }
    }
}
