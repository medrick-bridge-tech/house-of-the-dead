using System.Collections.Generic;
using UnityEngine;
using System;
public class ItemPuzzle : Puzzle 
{
    private int _placedItemCount;
    public string _selectedObject;
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
            CheckRequiredItems(_selectedObject);
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

    private void CheckRequiredItems(string selectedItem)
    {
        if (_requiredItems.Count == _placedItemCount)
        {
            HandlePuzzleFinish();
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void IncreasePlacedItemCount()
    {
        _placedItemCount++;
    }
}