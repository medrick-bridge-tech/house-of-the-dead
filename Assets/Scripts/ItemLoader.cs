using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class ItemLoader
{
    static public Sprite GetSprite(string itemName)
    {
        var requestedItem = Resources.Load<ItemData>("InventoryItemsData/"+ itemName);
        return requestedItem.sprite;
    }

    static public ItemData ConvertNameToItemData(string itemName)
    {
        var requestedItem = Resources.Load<ItemData>("InventoryItemsData/" + itemName);
        return requestedItem;
    }
}
