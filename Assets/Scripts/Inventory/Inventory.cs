using System;
using System.Collections.Generic;
using UnityEngine;
public class Inventory
{
    private Dictionary<string, int> items;

    public Action<Dictionary<string, int>> OnInventoryChange;

    public Inventory()
    {
        items = new Dictionary<string, int>();
    }

    public void AddItem(ItemData itemData)
    {
        string itemName = itemData.itemName;
        if (items.ContainsKey(itemName))
            items[itemName]++;
        else
            items.Add(itemName, 1);

        OnInventoryChange.Invoke(items);
    }

    public void RemoveItem(ItemData itemData)
    {
        string itemName = itemData.itemName;
        if (items.ContainsKey(itemName))
        {
            if (items[itemName] > 1)
                items[itemName]--;
            else
                items.Remove(itemName);
        }
        
        OnInventoryChange.Invoke(items);
    }

    public ItemData ReturnItemData(string ItemDataName)
    {
        ItemData _itemToBeReturned;
        if (items.ContainsKey(ItemDataName))
        {
            _itemToBeReturned = Resources.Load<ItemData>("InventoryItemsData/" + ItemDataName);
            return _itemToBeReturned;
        }
        else
        {
            return null;
        }
    }
}