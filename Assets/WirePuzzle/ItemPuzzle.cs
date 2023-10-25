using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPuzzle : Puzzle
{
    private string _selectedObject;
    [SerializeField] private List<ItemData> _requairedItems;
    [SerializeField] private bool IsSolved = false;
    private List<BoxCollider> _itemBoxCollider;
    private void Awake()
    {
        OnPuzzleSolved += OpenDoor;
        OnPuzzleFailed += CloseDoor;
        _itemBoxCollider = new List<BoxCollider>(_requairedItems.Count);
        for (int i = 0; i < _requairedItems.Count; i++)
        {
            _itemBoxCollider.Add(_itemBoxCollider[i].transform.GetChild(i).GetComponent<BoxCollider>());
            Debug.Log("BoxCollider added");
        }
    }    

    public override void HandlePuzzleFinish()
    {
        if (CheckItem("Fuse"))
        {
            
        }
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    foreach (BoxCollider boxCollider in _itemBoxCollider)
                    {
                        if (hit.collider == boxCollider)
                        {
                            Debug.Log("BoxCollider Touched!");
                        }
                    }
                }
            }
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

    private bool CheckItem(string selectedItem)
    {
        foreach (ItemData item in _requairedItems)
        {
            if (selectedItem == item.itemName)
                return true;
        }
        return false;
    }
}
