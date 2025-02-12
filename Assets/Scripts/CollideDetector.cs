using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideDetector : MonoBehaviour
{
    private Collider2D _collider;

    public Action<bool> CollisionChanged;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollisionChanged?.Invoke(true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        CollisionChanged?.Invoke(false);
    }
}
