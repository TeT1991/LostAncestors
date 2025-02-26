using System;
using System.Collections;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    private float _attackReload;
    private bool _canAttack;
    private Transform _projectile;
    private Transform _launchPoint;

    private Coroutine _coroutine;

    public float AttackReload => _attackReload;
    public bool CanAttack => _canAttack;

    public Action Reloaded;

    public void Init(float attackReload)
    {
        _attackReload = attackReload;
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
            _coroutine = StartCoroutine(Reload(_attackReload));
        }
    }

    public void SetProjectile(Transform projectile, Transform launchPoint)
    {
        _projectile = projectile;
        _launchPoint = launchPoint;
    }

    private IEnumerator Reload(float reloadTime)
    {
        yield return new WaitForSeconds(reloadTime);
        Reloaded?.Invoke();   
        _canAttack = true;
    }
}
