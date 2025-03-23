using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(WallDetector))]

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;
    private int _damage;

    private WaitForSeconds _wait;

    private CircleCollider2D _collider;
    private Mover _mover;
    private Coroutine _coroutine;

    public int Damage => _damage;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        _mover.MoveHorizontal(_speed);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            Destroy(gameObject);
        }
    }

    public void Init()
    {
        _damage = 1;
        _wait = new WaitForSeconds(_lifeTime);

        _collider = GetComponent<CircleCollider2D>();
        _mover = GetComponent<Mover>();
        _coroutine = StartCoroutine(DestroyByTime());
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private IEnumerator DestroyByTime()
    {
        yield return _wait;
        Destroy(gameObject);
    }
}
