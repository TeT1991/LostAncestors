using UnityEngine;

public class PickableDetector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out IPickable pickable))
        {
            pickable.PickUp();
        }
    }
}
