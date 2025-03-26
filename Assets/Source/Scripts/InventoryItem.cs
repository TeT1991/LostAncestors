using System;

public class InventoryItem : Item, ICloneable
{
    public object Clone()
    {
        return this.MemberwiseClone();
    }

    public override void PickUp()
    {
        base.PickUp();
    }
}