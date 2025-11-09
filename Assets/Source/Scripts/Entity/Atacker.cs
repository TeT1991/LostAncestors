using UnityEngine;

public class Atacker
{
    private readonly MonoBehaviour _monoBehaviour;
    private readonly Transform _projectileSpawnPoint;
    private readonly Projectile _projectilePrefab;
    private bool _canAttack;

    public Atacker(Transform projectileSpawnPoint, Projectile projectilePrefab)
    {
        _projectileSpawnPoint = projectileSpawnPoint;
        _projectilePrefab = projectilePrefab;
        _canAttack = true;
    }

    public void LaunchProjectile(float speed,float direction)
    {
        if (_canAttack)
        {
            Projectile projectile = Object.Instantiate(_projectilePrefab);
            projectile.Init(speed, direction);
            projectile.transform.position = _projectileSpawnPoint.position;
        }
    }
}
