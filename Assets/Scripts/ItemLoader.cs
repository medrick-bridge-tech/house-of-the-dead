using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLoader
{
    public Sprite GetSprite(string itemName)
    {
        var requestedItem = Resources.Load<ItemData>("Resources/InventoryItemsData/"+ itemName);
        return requestedItem.sprite;
    }
}
