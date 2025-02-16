using System.Collections;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    private GameObject _projectilePrefab;
    private float _attackReload;
    private bool _canAttack = true;

    private Coroutine _rangeAttackCoroutine;

    public float AttackReload => _attackReload;
    public bool CanAttack => _canAttack;

    public void Init(GameObject projectile, float attackReload)
    {
        _projectilePrefab = projectile;
        _attackReload = attackReload;
    }

    public void ApplyRangeAttack()
    {
        if (_canAttack)
        {
            var projectile = Instantiate(_projectilePrefab);
            projectile.transform.position = transform.position;
            _canAttack = false;
            _rangeAttackCoroutine = StartCoroutine(Reload(_attackReload));
        }
    }

    private IEnumerator Reload(float reloadTime)
    {
        Debug.Log("Reload");
        yield return new WaitForSeconds(reloadTime);
        _canAttack = true;
        Debug.Log("Reloaded");
    }
}
