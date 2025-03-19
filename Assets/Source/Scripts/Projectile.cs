using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(CircleCollider2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private OwnerType _ownerType;
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;
    private int _damage;

    private WaitForSeconds _wait;

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

    private void Update()
    {
        TryDestroyByCollide();
        _mover.Move(_speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(_ownerType== OwnerType.Character && collision.gameObject.TryGetComponent<IInteractable>(out IInteractable interactable))
        {
            interactable.Interact();
            Destroy();
        }
    }

    public void Init()
    {
        _damage = 1;
        _wait = new WaitForSeconds(_lifeTime);

        _collider = GetComponent<CircleCollider2D>();
        _mover = GetComponent<Mover>();
        _coroutine = StartCoroutine(DestroyByTime());

        _platformLayerMask = "Platform";
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

    private void TryDestroyByCollide()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(_collider.bounds.center, _collider.radius, LayerMask.GetMask(_platformLayerMask));

        if (hits.Length > 0)
        {
            Destroy(gameObject);
        }
    }
}
