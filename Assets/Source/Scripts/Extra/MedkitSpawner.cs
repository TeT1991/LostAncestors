using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class MedkitSpawner : MonoBehaviour
{
    [SerializeField] private Medkit _medkitPrefab;
    [SerializeField] private Transform _minSpawnPosition;
    [SerializeField] private Transform _maxSpawnPosition;

    private readonly float _spawnTime = 3f;
    private ObjectPool<Medkit> _pool;
    private Coroutine _coroutine;

    private void Awake()
    {
        _pool = new(
            createFunc: () => Instantiate(_medkitPrefab),
            actionOnGet: (obj) => ApplyActionOnGet(obj),
            actionOnRelease: (obj) => ApplyActionOnRelease(obj),
            actionOnDestroy: (obj) => Destroy(obj)
            );

        _coroutine = StartCoroutine(Spawn());
    }

    private void ApplyActionOnGet(Medkit coin)
    {
        coin.transform.position = CalculateSpawnPositoin();
        coin.gameObject.SetActive(true);
        coin.OnCollected += _pool.Release;
    }

    private void ApplyActionOnRelease(Medkit coin)
    {
        coin.gameObject.SetActive(false);
        coin.OnCollected -= _pool.Release;
    }

    private Vector2 CalculateSpawnPositoin()
    {
        float xPosition = Random.Range(_minSpawnPosition.position.x, _maxSpawnPosition.position.x);
        return new Vector3 (xPosition, transform.position.y, transform.position.z); 
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_spawnTime);

        while (enabled)
        {
            yield return waitForSeconds;
            _pool.Get();
        }
    }
}
