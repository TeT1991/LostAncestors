using UnityEngine;

public class PlayerProjectile : Projectile
{
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
       // base.OnCollisionEnter2D(collision);

        if (collision.gameObject.TryGetComponent<IInteractable>(out IInteractable interactable))
        {
            interactable.Interact();
            //Destroy();
        }
    }
}
