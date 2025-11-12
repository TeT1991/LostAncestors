public class Health 
{
    private readonly int _maxHealth;
    private int _currentHealth;

    public Health(int health, int maxHealth)
    {
        _currentHealth = health;
        _maxHealth = maxHealth;
    }

    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _maxHealth;

    public void IncreaseHealth()
    {
        _currentHealth++;
    }

    public void DecreaseHealth()
    {
        _currentHealth--;
    }
}
