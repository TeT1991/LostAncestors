using UnityEngine;

public class CoinDetector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<IPickable>(out IPickable pickable))
        {
            pickable.Pickup();
        }
    }
}
