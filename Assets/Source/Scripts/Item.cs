using System;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{    public virtual void PickUp()
    {
        Destroy(gameObject);
    }
}

public class ItemInfo
{
    private Image _icon;
}