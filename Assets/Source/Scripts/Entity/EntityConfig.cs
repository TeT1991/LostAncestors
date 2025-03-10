using UnityEngine;

[CreateAssetMenu(fileName = "NewEntityConfig", menuName = "Configs/Entity Config")]
public class EntityConfig : ScriptableObject
{
    [SerializeField] private int _health;

    [SerializeField] private float _groundSpeed;
    [SerializeField] private float _airHorizontalSpeed;
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _reloadTime;
    [SerializeField] private float _startDirection;

    [SerializeField] private Projectile _projectileRange;
    [SerializeField] private Projectile _projectileMelee;

    public int Health => _health;
    public float GroundSpeed => _groundSpeed;
    public float AirHorizontalSpeed => _airHorizontalSpeed;
    public float JumpPower => _jumpPower;
    public float ReloadTime => _reloadTime;
    public float StartDirection => _startDirection;
    public Projectile RangeProjectile => _projectileRange;
    public Projectile MeleeProjectile => _projectileMelee;
}
