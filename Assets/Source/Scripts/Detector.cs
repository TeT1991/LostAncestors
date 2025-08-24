using Spine.Unity;
using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Detector : MonoBehaviour
{
    public event Action<bool> OnCollided;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollided?.Invoke(true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        OnCollided?.Invoke(false);
    }
}
