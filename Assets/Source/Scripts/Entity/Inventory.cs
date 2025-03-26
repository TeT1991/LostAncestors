using System.Collections.Generic;

public class Inventory
{
    private List<Item> _items;
    private int _capacity = 4;

    public int Capacity => _capacity;
    public int ItemsCount => _items.Count;

    public void Init()
    {
        _items = new List<Item>();  
    }

    public void AddItem(InventoryItem item)
    {
        _items.Add(item);
    }

    public void RemoveItem(Item item)
    {
        _items.Remove(item);
    }
}
