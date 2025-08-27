using System;
using UnityEngine;

public class Coin : MonoBehaviour, IPickable
{
    public event Action<Coin> OnCollected;


    public void PickUp()
    {
       OnCollected?.Invoke(this);   
    }
}
