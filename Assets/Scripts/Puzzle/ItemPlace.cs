using UnityEngine;
using System;
public class ItemPlace : MonoBehaviour
{
    [SerializeField] string _itemName;
    [SerializeField] private InventoryUI _inventoryUI;
    [SerializeField] private ItemPuzzle _itemPuzzle;
    [SerializeField] private LayerMask _layrMask;
    

    public event Action OnItemAdded;

    private void Awake()
    {
        OnItemAdded += _itemPuzzle.IncreasePlacedItemCount;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layrMask))
            {
                if (_inventoryUI.SelectedItem == _itemName && hit.collider.gameObject == gameObject)
                {
                    void TurnOnMesh()
                    {
                        gameObject.GetComponent<MeshRenderer>().enabled = true;
                        ItemData item = ItemLoader.ConvertNameToItemData(_itemName);
                        _inventoryUI.Inventory.RemoveItem(item);
                    }
                    TurnOnMesh();
                    OnItemAdded.Invoke();
                }
            }
        }
    }
}
