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
        _onSolveAnimation.SetTrigger("SolveAnim");
        if (_listener != null)
            _listener.Play();
        if (_onStartPuzzleAnimation != null)
        {
            _onStartPuzzleAnimation.SetBool("StartAnim", false);
        }
    }

    protected override void Update()
    {
        base.Update();
        if (!_isSolved && InventoryUI.Instance.SelectedItem != null)
        {
            _selectedObject = InventoryUI.Instance.SelectedItem;
            CheckRequiredItems(_selectedObject);
        }
    }

    protected override void HandleCharacterInteraction(Character character)
    {
        if (!_isSolved)
        {
            base.HandleCharacterInteraction(character);
            _myInventory = character.Inventory;
            if (_onStartPuzzleAnimation != null)
            {
                _onStartPuzzleAnimation.SetBool("StartAnim", true);
            }
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