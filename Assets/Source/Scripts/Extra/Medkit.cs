using System;
using UnityEngine;

public class Medkit : MonoBehaviour, IPickable
{
    private readonly PickableType _pickableType = PickableType.Medkit;

    public event Action<Medkit> OnCollected;
    public void PickUp()
    {
        Debug.Log("PICKUP");
       OnCollected?.Invoke(this);   
    }

    public PickableType GetPickableType()
    {
       return _pickableType;
    }
}
