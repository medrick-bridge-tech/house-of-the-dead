using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory inventory;

    public List<GameObject> slots;
    private List<string> slotItemNames;
    public Button actionButton;
    public Button bagButton;
    private ItemLoader itemLoader;

    private void Awake()
    {
        itemLoader = new ItemLoader();
        //inventory.OnInventoryChange += UpdateSlots;
        foreach (var slot in slots)
        {
            //slot.GetComponent<Button>().onClick.AddListener(SelectItem);
        }
    }
    
    public void UpdateSlots(Dictionary<string, int> items)
    {
        //ClearAllSlots();
        int i = 0;
        foreach (var item in items)
        {
            var itemName = item.Key;
            // TODO: Set the count UI to item.Value
            Image slotImage = slots[i].transform.GetChild(1).GetComponent<Image>();
            slots[i].transform.GetChild(1).name = itemName;
            slotImage.sprite = itemLoader.GetSprite(itemName);
            i++;
            return;
        }
    }

    private void ClearAllSlots()
    {
        foreach (var slot in slots)
        {
            slot.GetComponentInChildren<Image>().sprite = null;
        }
    }

    public string SelectItem(string itemName)
    {
        if (slotItemNames.Contains(itemName))
            return itemName;
        else
            return itemName;
    }
    
}
