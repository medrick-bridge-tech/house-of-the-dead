using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
public class ItemPuzzle : Puzzle
{
    [SerializeField] private List<ItemData> _requiredItems;
    [SerializeField] private Animator _onSolveAnimation;
    [SerializeField] private Animator _onStartPuzzleAnimation;
    [SerializeField] private AudioSource _listener;

    private int _placedItemCount;
    private string _selectedObject;
    private bool _isSolved = false;
    private Inventory _myInventory;

    protected override void Awake()
    {
        base.Awake();
        OnPuzzleSolved += OpenDoor;
        if (_listener == null)
        {
            _listener = gameObject.GetComponent<AudioSource>(); 
        }
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
        Vector3 newPosition = character.transform.position + new Vector3(-0.4f, 0, 0);
        character.transform.DOMove(newPosition , 1f);
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