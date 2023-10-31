using System.Collections.Generic;
using UnityEngine;

public class ItemPuzzle : Puzzle 
{
    private string _selectedObject;
    private bool _isSolved = false;
    private Inventory _myInventory;

    [SerializeField] private List<ItemData> requiredItems;
    [SerializeField] private Character _characterInventory;
    [SerializeField] private InventoryUI _inventoryUI;
    [SerializeField] private Animator _solveAnimator;
    

    private void Start()
    {
        OnPuzzleSolved += OpenDoor;
        _myInventory = _characterInventory.Inventory;  
    }    

    public override void HandlePuzzleFinish()
    {
        _isSolved = true;
        OnPuzzleSolved.Invoke();
    }
    
    private void OpenDoor()
    {
        _solveAnimator.SetTrigger("RoomOneTrigger");
    }

    private void Update()
    {
        if (!_isSolved && _inventoryUI.SelectedItem != null)
        {
            _selectedObject = _inventoryUI.SelectedItem;
            CheckRequiredItems(_selectedObject);
        }
    }

    private void CheckRequiredItems(string selectedItem)
    {
        foreach (ItemData item in requiredItems)
        {
            if (selectedItem == item.itemName)
            {
                requiredItems.Remove(item);
                _myInventory.RemoveItem(item);
                break;
            }
        } 
        if (requiredItems.Count == 0)
        {
            HandlePuzzleFinish();
            Debug.Log("Finish");
        }
    }
}