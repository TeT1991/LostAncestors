using System;

public class Health 
{
    private readonly float _maxHealth;
    private float _currentHealth;

    public event Action<float> HealthChanged;

    public Health(float health, float maxHealth)
    {
        _currentHealth = health;
        _maxHealth = maxHealth;
    }

    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _maxHealth;

    public void IncreaseHealth(float value)
    {
        _currentHealth =  _currentHealth + value > _maxHealth ? _maxHealth : _currentHealth + value;
        HealthChanged?.Invoke(_currentHealth);
    }

    public void DecreaseHealth(float value)
    {
        _currentHealth = _currentHealth - value < 0 ? 0 : _currentHealth - value;
        HealthChanged?.Invoke(_currentHealth);
    }
}
