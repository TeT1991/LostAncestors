using System;
using UnityEngine;

public class InteractableDetector : Detector
{
    public event Action<IInteractable> Collided;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out IInteractable interactable))
        {
            Collided?.Invoke(interactable);
            interactable.ShowMessage();
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IInteractable interactable))
        {
            Collided?.Invoke(interactable);
            interactable.HideMessage();
        }
    }
}
