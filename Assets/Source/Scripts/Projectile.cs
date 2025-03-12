using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(CircleCollider2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private OwnerType _ownerType;
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;

    private WaitForSeconds _lifeTime;

    private CircleCollider2D _collider;
    private Mover _mover;
    private Coroutine _coroutine;

    private string _platformLayerMask;

    public OwnerType OwnerType => _ownerType;
    public int Damage => _damage;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        _speed = 10;
        _damage = 1;
        _lifeTime = new WaitForSeconds(3);

        _collider = GetComponent<CircleCollider2D>();
        _mover = GetComponent<Mover>();
        _coroutine = StartCoroutine(DestroyByTime());

        _platformLayerMask = "Platform";
    }

    private void Update()
    {
        TryDestroyByCollide();
        _mover.Move(_speed);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private IEnumerator DestroyByTime()
    {
        yield return _lifeTime;
        Destroy(gameObject);
    }

    private void TryDestroyByCollide()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(_collider.bounds.center, _collider.radius, LayerMask.GetMask(_platformLayerMask));

        if (hits.Length > 0)
        {
            Destroy(gameObject);
        }
    }
}
