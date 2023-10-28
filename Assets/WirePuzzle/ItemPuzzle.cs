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
    private void Awake()
    {
        OnPuzzleSolved += OpenDoor;
        OnPuzzleFailed += CloseDoor;
    }    

    public override void HandlePuzzleFinish()
    {
        bool allActive = false;

        foreach (GameObject obj in objectList)
        {
            if (obj.activeSelf)
            {
                allActive = true;
            }
            else
            {
                allActive = false;
                break;
            }
        }

        if (allActive)
        {
            Debug.Log("Puzzle Solved");
            _isSolved = true;
            OnPuzzleSolved.Invoke();
        }
        else
        {
            Debug.Log("Puzzle Failed");
            OnPuzzleFailed.Invoke();
        }
    }
    
    private void OpenDoor()
    {
        //Audio clip of electrician access play
        //Door becomes open and event handler unsubscribe 
        OnPuzzleSolved?.Invoke();
    }

    private void CloseDoor()
    {
        //Audio clip of electrician fail play
        //Door remain Close
        OnPuzzleFailed?.Invoke();
    }
    private void Update()
    {
        if (!_isSolved)
        {
            _selectedObject = _inventoryMock.SelectedItem;
            PutObjects();
            if (objectList.Count == 0)
                HandlePuzzleFinish();
            Debug.Log(objectList.Count);
        }
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
                    objectList.Remove(obj);
                    break;
                }
                break;
            }
        }
    }
}
