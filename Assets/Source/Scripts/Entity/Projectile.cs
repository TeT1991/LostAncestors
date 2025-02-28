using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private OwnerType _ownerType;
    [SerializeField] private float _speed = 10;
    [SerializeField] private float _lifeTime = 3f;
    private int _damage = 1;

    private Mover _mover;
    private Coroutine _coroutine;

    public OwnerType OwnerType => _ownerType;
    public int Damage => _damage;   

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        _mover = GetComponent<Mover>();
        _coroutine = StartCoroutine(DestroyByTime());
    }

    private void Update()
    {
        _mover.Move(_speed);
    }


    private IEnumerator DestroyByTime()
    {
        yield return new WaitForSeconds(_lifeTime);
        Destroy(gameObject);
    }
}
