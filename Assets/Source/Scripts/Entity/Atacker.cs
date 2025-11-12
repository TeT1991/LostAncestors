using System.Collections;
using UnityEngine;

public class Atacker
{
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly Transform _projectileSpawnPoint;
    private readonly Projectile _projectilePrefab;

    private bool _canAttack;
    private WaitForSeconds _waitForAttackReload;
    private Coroutine _coroutine;

    public Atacker(ICoroutineRunner coroutineRunner, Transform projectileSpawnPoint, Projectile projectilePrefab, float reloadTime)
    {
        _coroutineRunner = coroutineRunner;
        _projectileSpawnPoint = projectileSpawnPoint;
        _projectilePrefab = projectilePrefab;
        _canAttack = true;
        _waitForAttackReload = new(reloadTime);
    }

    public void LaunchProjectile(float speed,float direction)
    {
        if (_canAttack)
        {
            Projectile projectile = Object.Instantiate(_projectilePrefab);
            projectile.Init(speed, direction);
            projectile.transform.position = _projectileSpawnPoint.position;
            _canAttack = false;
            _coroutine = _coroutineRunner.StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        yield return _waitForAttackReload;

        _canAttack = true;
    }
}
