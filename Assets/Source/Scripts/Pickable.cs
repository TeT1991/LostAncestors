using UnityEngine;

public class Pickable : MonoBehaviour
{
    [SerializeField]private PickableType _pickableType;

    public PickableType PickableType => _pickableType;

    public void PickUp()
    {
        Destroy(gameObject);
    }
}
