using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
   [SerializeField] private List<Enemy> _enemies;

    private void Awake()
    {
        Init();
    }

    private void OnDestroy()
    {
        foreach (Enemy enemy in _enemies)
        {
            enemy.HealthOver -= Destroy;
        }
    }

    private void Init()
    {
        foreach (Enemy enemy in _enemies)
        {
            enemy.HealthOver += Destroy;
        }
    }

    private void Destroy(Enemy enemy)
    {
        enemy.HealthOver -= Destroy;
        Destroy(enemy.gameObject);
    }
}
