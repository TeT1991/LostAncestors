using UnityEngine;

[RequireComponent(typeof(InteractableDetector))]
public class PlayerProjectile : Projectile
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.gameObject.TryGetComponent<IInteractable>(out IInteractable interactable))
        {
            interactable.Interact();
            Destroy();
        }
    }
}
