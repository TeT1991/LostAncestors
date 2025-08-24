using System;
using UnityEngine;

public class Coin : MonoBehaviour, IPickable
{
    public event Action<Coin> OnCollided;

    public void Pickup()
    {
        OnCollided?.Invoke(this);
    }
}

public interface IPickable
{
    public void Pickup();
}
