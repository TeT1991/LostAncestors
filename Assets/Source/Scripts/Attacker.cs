using System.Collections;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    private Transform _projectilePrefab;
    private float _attackReload;
    private bool _canAttack = true;

    private Coroutine _coroutine;

    public float AttackReload => _attackReload;
    public bool CanAttack => _canAttack;

    public void Init(Transform projectile, float attackReload)
    {
        _projectilePrefab = projectile;
        _attackReload = attackReload;
    }

    public void ApplyRangeAttack(float direction)
    {
        if (_canAttack)
        {
            var projectile = Instantiate(_projectilePrefab);
            projectile.transform.position = transform.position;
            projectile.transform.right = transform.right * direction;
            _canAttack = false;
            _coroutine = StartCoroutine(Reload(_attackReload));
        }
    }

    private IEnumerator Reload(float reloadTime)
    {
        yield return new WaitForSeconds(reloadTime);
        _canAttack = true;
    }
}
