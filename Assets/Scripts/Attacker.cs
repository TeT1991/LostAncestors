using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    private GameObject _projectilePrefab;

    public void Init(GameObject projectile)
    {
        _projectilePrefab = projectile;
    }

    public void ApplyRangeAttack()
    {
        var projectile = Instantiate(_projectilePrefab);
        projectile.transform.position = transform.position;
    }
}
