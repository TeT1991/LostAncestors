using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterConfig", menuName = "Configs/Character Config")]
public class EntityConfig : ScriptableObject
{
    [SerializeField]private float _groundSpeed;
    [SerializeField] private float _airHorizontalSpeed;
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _reloadTime;
    [SerializeField] private float _startDirection;

    [SerializeField] private Transform _projectile;

    public float GroundSpeed => _groundSpeed;
    public float AirHorizontalSpeed => _airHorizontalSpeed;
    public float JumpPower => _jumpPower;
    public float ReloadTime => _reloadTime;
    public float StartDirection => _startDirection;
    public Transform Projectile => _projectile;
}
