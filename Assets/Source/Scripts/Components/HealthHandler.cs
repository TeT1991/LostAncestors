using System;
using UnityEngine;

public class HealthHandler 
{
    private int _maxHealth;
    public int _health;

    public int MaxHealth => _maxHealth;
    public int Health => _health;

    public Action Died;
    public void Init(int health)
    {
        _maxHealth = health;
        _health = _maxHealth;
    }

    public void ApplyDamage(int value)
    {
        if(value > 0)
        {
            _health -= value;

            if(_health <= 0)
            {
                Died?.Invoke();
            }
        }
    }

    public void ApplyHeal(int value)
    {
        if (value > 0)
        {
            _health += _health < _maxHealth ? value : 0;
        }
    }
}
