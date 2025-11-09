using System;
using UnityEngine;

public class PickableDetector : MonoBehaviour
{
    public event Action<IPickable> OnPicked;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out IPickable pickable))
        {
            OnPicked?.Invoke(pickable);
        }
    }
}
