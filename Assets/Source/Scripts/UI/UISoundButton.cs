using System;
using UnityEngine;
using UnityEngine.UI;

public class UISoundButton : MonoBehaviour
{
    [SerializeField] private int _soundIndex;
    [SerializeField] private Button _button;

    public event Action<int> ButtonPressed;

    public void Init()
    {
        _button.onClick.AddListener(NotifyButtonPressed);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }

    public void NotifyButtonPressed()
    {
        ButtonPressed?.Invoke(_soundIndex);
    }
}
