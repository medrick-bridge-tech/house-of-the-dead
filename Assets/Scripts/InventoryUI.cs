
using System;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Character owner;

    public List<ItemPresenter> slots;
    public Button actionButton;
    public Button bagButton;
    private ItemLoader _itemLoader;
    private Inventory _inventory;
    [SerializeField] private InventoryAnimation _inventoryAnimation;
    [SerializeField] private GameObject zoomPanel;
    private Image _zoomPanelImage;


    public string SelectedItem { get; private set; }

    private void Start()
    {
        _inventory = owner.Inventory;
        _itemLoader = new ItemLoader();
        _inventory.OnInventoryChange += UpdateSlots;
        _zoomPanelImage = zoomPanel.transform.GetChild(0).transform.GetChild(1).GetComponent<Image>();
        for (var i = 0; i < slots.Count; i++)
        {
            var t = i;
            slots[i].GetComponentInChildren<Button>().onClick.AddListener(() => SelectItemAtIndex(t));
        }
    }

    private void UpdateSlots(Dictionary<string, int> items)
    {
        ClearAllSlots();

        int i = 0;
        foreach (var item in items)
        {
            var itemName = item.Key;
            slots[i].Setup(itemName, _itemLoader.GetSprite(itemName));
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
        SelectedItem = slots[index].ItemName;
        if (_inventoryAnimation.IsMagnifierSelected)
        {
            zoomPanel.transform.GetChild(0).gameObject.SetActive(true);
            var sprite = slots[index].transform.GetChild(1).GetComponent<Image>().sprite;
            _zoomPanelImage.sprite = sprite;
        }
    }

    public void HideZoomPanel()
    {
        zoomPanel.transform.GetChild(0).gameObject.SetActive(false);
        _inventoryAnimation.MagnifierSelected();
    }
}
