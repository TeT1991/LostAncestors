using System;
using System.Collections;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    private float _attackReloadTime;
    private bool _canAttack;
    private Projectile _projectile;
    private Transform _launchPoint;

    private Coroutine _coroutine;
    private WaitForSeconds _reload;

    public Action Reloaded;

    public void Init(float attackReloadTime)
    {
        _reload = new WaitForSeconds(attackReloadTime);
        _canAttack = true;
    }

    public void ApplyAttack(float direction)
    {
        if (_canAttack)
        {
            var projectile = Instantiate(_projectile);
            projectile.transform.position = _launchPoint.position;
            projectile.transform.right = transform.right * direction;
            _canAttack = false;
            _coroutine = StartCoroutine(Reload(_attackReloadTime));
        }
    }

    public void SetProjectile(Projectile projectile, Transform launchPoint)
    {
        _projectile = projectile;
        _launchPoint = launchPoint;
    }

    private IEnumerator Reload(float reloadTime)
    {
        yield return _reload;
        Reloaded?.Invoke();   
        _canAttack = true;
    }
}
