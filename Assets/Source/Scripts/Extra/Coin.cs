using UnityEngine;

public class Coin : MonoBehaviour, IPickable
{
    public void PickUp()
    {
       Destroy(gameObject);
    }
}
