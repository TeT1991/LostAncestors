using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBootstrap : MonoBehaviour
{
    [SerializeField] private AudioSystem _audioSystem;
    [SerializeField] private Button _button;

    [SerializeField] private List<UISoundButton> _soundButtons;
    [SerializeField] private List<AudioMenuItem> _menuItems;


    private void Start()
    {
        foreach(var soundButton in _soundButtons)
        {
            soundButton.ButtonPressed += _audioSystem.PlaySound;
            soundButton.Init();
        }

        foreach(var menuItem in _menuItems)
        {
            menuItem.VolumeChaged += _audioSystem.SetVolume;
            menuItem.Init();
        }

        _button.onClick.AddListener(_audioSystem.ToggleAudio);
    }
}
