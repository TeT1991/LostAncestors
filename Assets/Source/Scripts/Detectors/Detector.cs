using System;
using UnityEngine;

public class Detector : MonoBehaviour
{
    protected Collider2D Collider;

    protected virtual void OnTriggerEnter2D(Collider2D collision) { }
    protected virtual void OnTriggerExit2D(Collider2D collision) { }

    public virtual void Init()
    {
        Collider = GetComponent<Collider2D>();
    }

    public virtual void Init(Collider2D collider)
    {
        Collider = collider;
    }
}
