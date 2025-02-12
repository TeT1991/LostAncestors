using UnityEngine;

[RequireComponent (typeof(Mover))]
public class Projectile : MonoBehaviour
{
    private float _speed = 0.5f;
    private Mover _mover;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        _mover = GetComponent<Mover>();
    }

    private void Update()
    {
        _mover.Move(_speed * Time.deltaTime);
    }
}
