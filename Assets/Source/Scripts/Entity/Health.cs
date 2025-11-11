public class Health 
{
    private int _currentHealth;
    private int _maxHealth;

    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _maxHealth;

    public Health(int health, int maxHealth)
    {
        _currentHealth = health;
        _maxHealth = maxHealth;
    }

    public void IncreaseHealth()
    {
        _currentHealth++;
    }

    public void DecreaseHealth()
    {
        _currentHealth--;
    }
}
