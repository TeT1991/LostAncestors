using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterConfig", menuName = "Configs/Character Config")]
public class EntityConfig : ScriptableObject
{
    [SerializeField] private int _health;

    [SerializeField] private float _groundSpeed;
    [SerializeField] private float _airHorizontalSpeed;
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _reloadTime;
    [SerializeField] private float _startDirection;

    [SerializeField] private Transform _projectileRange;
    [SerializeField] private Transform _projectileMelee;

    public int Health => _health;
    public float GroundSpeed => _groundSpeed;
    public float AirHorizontalSpeed => _airHorizontalSpeed;
    public float JumpPower => _jumpPower;
    public float ReloadTime => _reloadTime;
    public float StartDirection => _startDirection;
    public Transform RangeProjectile => _projectileRange;
    public Transform MeleeProjectile => _projectileMelee;
}
