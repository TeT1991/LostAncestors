using System;
using UnityEngine;

public class InteractableDetector : Detector
{
    public event Action<IInteractable> Colided;
    public event Action TriggerExit;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out IInteractable interactable))
        {
            Colided?.Invoke(interactable);
            interactable.ShowMessage();
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IInteractable interactable))
        {
            Colided?.Invoke(null);
            interactable.HideMessage();
        }
    }
}
