using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WirePuzzle : Puzzle
{
    public bool IsSolved = false;
    private void Awake()
    {
        OnPuzzleSolved += OpenDoor;
        OnPuzzleFailed += CloseDoor;
    }
    
    public override void HandlePuzzleFinish()
    {
        if(DoorState())
            OpenDoor();
        else
            CloseDoor();
    }

    private void Update()
    {
        DoorState();
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

    private bool DoorState()
    {
        if(true)
            return true;
        else
            return false;
    }
}
