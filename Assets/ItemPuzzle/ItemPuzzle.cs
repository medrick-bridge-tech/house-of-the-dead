using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemPuzzle : Puzzle
{
    private string _selectedObject;
    private bool _isSolved = false;
    [SerializeField] private List<ItemData> requairedItems;
    [SerializeField] private List<GameObject> objectList;
    [SerializeField] private InventoryMock _inventoryMock;
    [SerializeField] private string _animationTrigger;
    [SerializeField] private Animator _solveAnimator;
    private void Awake()
    {
        OnPuzzleSolved += OpenDoor;
        OnPuzzleFailed += CloseDoor;
    }    

    public override void HandlePuzzleFinish()
    {
        if (objectList.Count() == 0)
        {
            _isSolved = true;
            OnPuzzleSolved.Invoke();
        }
        else
        {
            OnPuzzleFailed.Invoke();
        }
    }
    
    private void OpenDoor()
    {
        _solveAnimator.SetTrigger(_animationTrigger);
    }

    private void CloseDoor()
    {

    }

    private void Update()
    {
        if (!_isSolved)
        {
            _selectedObject = _inventoryMock.SelectedItem;
            PutObjects();
        }
    }

    private void PutObjects()
    {
        int itemNumber = requairedItems.Where(x => x.itemName == _selectedObject).Count();
        if(itemNumber > 0)
        {
            GameObject obj = objectList.FirstOrDefault(x => x.activeSelf == false) ;
            if (obj != null)
            {
                obj.SetActive(true);
                objectList.Remove(obj);
            }
            if (objectList.Count() == 0)
                HandlePuzzleFinish();
        }
    }
}