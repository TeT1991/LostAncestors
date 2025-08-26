using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private Transform _minSpawnPosition;
    [SerializeField] private Transform _maxSpawnPosition;

    private readonly float _spawnTime = 3f;
    private ObjectPool<Coin> _pool;
    private Coroutine _coroutine;

    private void Awake()
    {
        _pool = new(
            createFunc: () => Instantiate(_coinPrefab),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false)
            );

        _coroutine = StartCoroutine(Spawn());
    }

    private void ActionOnGet(Coin coin)
    {
        coin.transform.position = CalculateSpawnPositoin();
        coin.gameObject.SetActive(true);
    }

    private Vector2 CalculateSpawnPositoin()
    {
        float xPosition = Random.Range(_minSpawnPosition.position.x, _maxSpawnPosition.position.x);
        return new Vector3 (xPosition, transform.position.y, transform.position.z); 
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds (_spawnTime);
            _pool.Get();
        }
    }
}
