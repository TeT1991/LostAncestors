using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Pool;

public class CoinsSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private Transform _minSpawnPosition;
    [SerializeField] private Transform _maxSpawnPosition;

    private readonly float _spawnTime = 3f;
    private ObjectPool<Coin> _pool;
    private Coroutine _coroutine;


    private void Awake()
    {
        _pool = new ObjectPool<Coin>(
            createFunc: () => Instantiate(_coinPrefab),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj.gameObject)
            );

        _coroutine = StartCoroutine(Spawn());
    }

    private void ActionOnGet(Coin coin)
    {
        coin.transform.position = CalculateRandomSpawnPoint();
        coin.OnCollided += ReleaseCoin;
        coin.gameObject.SetActive(true);
    }

    private Vector2 CalculateRandomSpawnPoint()
    {
        float x = Random.Range(_minSpawnPosition.transform.position.x, _maxSpawnPosition.transform.position.x);
        float y = transform.position.y;

        return new Vector2(x, y);
    }

    private void ReleaseCoin(Coin coin)
    {
        coin.OnCollided -= ReleaseCoin;
        _pool.Release(coin);
    }

    public IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnTime);
            _pool.Get();
        }
    }
}
