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

    public void IncreaseHealth()
    {
        _currentHealth++;
        HealthChanged?.Invoke(_currentHealth);
    }

    public void DecreaseHealth()
    {
        _currentHealth--;
        HealthChanged?.Invoke(_currentHealth);
    }
}
