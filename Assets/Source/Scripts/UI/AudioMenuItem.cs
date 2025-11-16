using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMenuItem : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private AudioMixerGroup _audioMixerGroup;

    public Slider Slider => _slider;
    public AudioMixerGroup AudioMixerGroup => _audioMixerGroup;

    public event Action<float, AudioMixerGroup> VolumeChaged;

    public void Init()
    {
        float startSliderValue = 0.5f;
        _slider.onValueChanged.AddListener(NotifyVolumeChaged); 
        _slider.value = startSliderValue;
        NotifyVolumeChaged(startSliderValue);
    }

    private void OnDestroy()
    {
        _slider.onValueChanged.RemoveAllListeners();
    }

    public void NotifyVolumeChaged(float value)
    {
        VolumeChaged.Invoke(value, _audioMixerGroup);
    }
}
