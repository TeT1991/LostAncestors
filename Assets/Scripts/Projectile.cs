using System.Collections;
using UnityEngine;

[RequireComponent (typeof(Mover))]
public class Projectile : MonoBehaviour
{
    private float _speed = 5;
    private Mover _mover;
    private float _lifeTime = 3f;
    private Coroutine _coroutine;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        _mover = GetComponent<Mover>();
        _coroutine = StartCoroutine(Destroy());
    }

    private void Update()
    {
        _mover.Move(_speed );
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(_lifeTime);
        Destroy(gameObject);
    }
}
