using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ItemPuzzle : Puzzle
{
    private string _selectedObject;
    private bool _isSolved = false;
    [SerializeField] private List<ItemData> requairedItems;
    [SerializeField] private List<GameObject> objectList;
    [SerializeField] private InventoryMock _inventoryMock;
    [SerializeField] private int _itemNumberToBeSolved;
    [SerializeField] private Animator _solveAnimator;
    private void Awake()
    {
        OnPuzzleSolved += OpenDoor;
        OnPuzzleFailed += CloseDoor;
    }    

    public override void HandlePuzzleFinish()
    {
        if (_itemNumberToBeSolved == 0)
        {
            Debug.Log("Puzzle Solved");
            _isSolved = true;
            OnPuzzleSolved.Invoke();
        }
/*        else
        {
            Debug.Log("Puzzle Failed");
            OnPuzzleFailed.Invoke();
        }*/
    }
    
    private void OpenDoor()
    {
        _solveAnimator.SetTrigger("RoomOneTrigger");
        //Audio clip of electrician access play
        //Door becomes open and event handler unsubscribe 
    }

    private void CloseDoor()
    {
        //Audio clip of electrician fail play
        //Door remain Close
    }
    private void Update()
    {
        if (!_isSolved)
        {
            _selectedObject = _inventoryMock.SelectedItem;
            PutObjects();
        }
/*        Debug.Log("Solve :" + _isSolved + " Needed GameObjects :" + 
            objectList.Count + " Selected Object is: " + _selectedObject 
            + " Took correct item : " + CheckItem(requairedItems[0]));*/
    }

    private bool CheckItem(ItemData selectedItem)
    {
        if (_selectedObject == selectedItem.itemName)
        {
            Debug.Log("Correct Oject");
            return true;
        }
        else
            return false;
    }

    private void PutObjects()
    {
        foreach (ItemData item in requairedItems)
        {
            if (CheckItem(item))
            {
                foreach (GameObject obj in objectList)
                {
                    obj.SetActive(true);
                    _itemNumberToBeSolved--;
                    break;
                }
                if(_itemNumberToBeSolved == 0)
                    HandlePuzzleFinish();
                break;
            }
        }
    }
}
