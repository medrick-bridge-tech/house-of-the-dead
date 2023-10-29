using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory
{
    private Dictionary<string, int> items = new Dictionary<string, int>();

    //public event Action<Dictionary<string, int>> OnInventoryChange;

    public void AddItem(ItemData itemData)
    {
        string itemName = itemData.itemName;
        if (items.ContainsKey(itemName))
            items[itemName]++;
        else
            items.Add(itemName, 1);

        InventoryUI inventoryUI = GameObject.Find("Inventory").GetComponent<InventoryUI>();
        inventoryUI.UpdateSlots(items);
        //OnInventoryChange.Invoke(items);
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
        
        //OnInventoryChange.Invoke(items);
    }
}