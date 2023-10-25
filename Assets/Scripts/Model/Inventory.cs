using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private Dictionary<string, int> items = new Dictionary<string, int>();

    public void AddItem(ItemData itemData)
    {
        string itemName = itemData.itemName;
        if (items.ContainsKey(itemName))
            items[itemName]++;
        else
            items.Add(itemName, 1);;
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
    }

    public bool Search(string itemName)
    {
        if (items.ContainsKey(itemName))
            return true;
        else
            return false;
    }
}