using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSystem : MonoBehaviour
{
    [SerializeField] private AudioSource _musicAudioSource;
    [SerializeField] private AudioSource _soundAudioSource;
    [SerializeField] private List<AudioClip> _sounds;
    public void PlaySound(int index)
    {
        if(_soundAudioSource.clip != _sounds[index])
        {
            _soundAudioSource.clip = _sounds[index];
        }

        _soundAudioSource.Play();
    }

    public void SetVolume(float value, AudioMixerGroup audioMixerGroup)
    {
        string paramName = "Volume" + audioMixerGroup.name;
        float maxValue = -80f;
        float minValue = 0f;
        audioMixerGroup.audioMixer.SetFloat(paramName, Mathf.Lerp(maxValue, minValue, value));
    }

    public void ToggleAudio()
    {
        _musicAudioSource.mute = !_musicAudioSource.mute;
    }
}
