using System.Collections.Generic;
using UnityEngine;

public class ItemPuzzle : Puzzle 
{
    private string _selectedObject;
    private bool _isSolved = false;
    private AudioSource _listener;
    private Inventory _myInventory;

    [SerializeField] private List<ItemData> _requiredItems;
    [SerializeField] private InventoryUI _inventoryUI;
    [SerializeField] private Animator _onSolveAnimation;
    [SerializeField] private Animator _onStartPuzzleAnimation;

    protected override void Awake()
    {
        base.Awake();
        OnPuzzleSolved += OpenDoor;
        _listener = gameObject.GetComponent<AudioSource>(); 
    }    

    public override void HandlePuzzleFinish()
    {
        _isSolved = true;
        OnPuzzleSolved.Invoke();
    }
    
    private void OpenDoor()
    {
        _onSolveAnimation.SetTrigger("RoomOneTrigger");
        _listener.Play();
        _onStartPuzzleAnimation.SetBool("OpenDoor", false);
    }

    protected override void Update()
    {
        base.Update();
        if (!_isSolved && _inventoryUI.SelectedItem != null)
        {
            _selectedObject = _inventoryUI.SelectedItem;
            Check_requiredItems(_selectedObject);
        }
    }

    protected override void HandleCharacterInteraction(Character character)
    {
        if (!_isSolved)
        {
            base.HandleCharacterInteraction(character);
            _myInventory = character.Inventory;
            _onStartPuzzleAnimation.SetBool("OpenDoor", true);
        }
    }

    private void Check_requiredItems(string selectedItem)
    {
        foreach (ItemData item in _requiredItems)
        {
            if (selectedItem == item.itemName)
            {
                _requiredItems.Remove(item);
                _myInventory.RemoveItem(item);
                break;
            }
        } 
        if (_requiredItems.Count == 0)
        {
            HandlePuzzleFinish();
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}