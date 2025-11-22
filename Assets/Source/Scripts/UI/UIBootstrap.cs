using UnityEngine;

public class UIBootstrap : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private UIValueBarHolder _healthBar;

    private void Start()
    {

        float maxValue = _character.Health.MaxHealth;
        float currentValue = _character.Health.CurrentHealth;


        //_character.Health.HealthChanged += _healthBar.ChangeValues;
    }
}
