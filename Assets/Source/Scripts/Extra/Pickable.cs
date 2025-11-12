using System;
using UnityEngine;

public class Pickable : MonoBehaviour, IPickable
{
    [SerializeField] private PickableType _pickableType = PickableType.Medkit;

    public event Action<Pickable> OnCollected;
    public void PickUp()
    {
       OnCollected?.Invoke(this);   
    }

    public PickableType GetPickableType()
    {
       return _pickableType;
    }
}
