using System;
using UnityEngine;

public class CharacterSwitcher : MonoBehaviour
{
    [SerializeField] private Character _currentCharacter;

    public Character CurrentCharacter => _currentCharacter;

    public Action<Entity> CharacterSwitched;

    public void SwitchCharacter()
    {
        CharacterSwitched?.Invoke(_currentCharacter);
    }
}
