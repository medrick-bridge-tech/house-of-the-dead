using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;
public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Character owner;
    private Inventory inventory;

    public List<ItemPresenter> slots;
    public Button actionButton;
    public Button bagButton;
    private ItemLoader itemLoader;

    public string SelectedItem { get; private set; }

    private void Start()
    {
        inventory = owner.Inventory;
        itemLoader = new ItemLoader();
        inventory.OnInventoryChange += UpdateSlots;
        for (var i = 0; i < slots.Count; i++)
        {
            var slot = slots[i];
            var t = i;
            slot.GetComponentInChildren<Button>().onClick.AddListener(() => SelectItemAtIndex(t));
        }
    }

    public void UpdateSlots(Dictionary<string, int> items)
    {
        ClearAllSlots();
        int i = 0;
        foreach (var item in items)
        {
            var itemName = item.Key;
            slots[i].Setup(itemName, itemLoader.GetSprite(itemName));
            i++;
        }
    }

    private void ClearAllSlots()
    {
        foreach (var slot in slots)
            slot.Clear();
        
    }

    private void SelectItemAtIndex(int index)
    {
        SelectedItem = slots[index].ItemName; ;
    }
    
}
