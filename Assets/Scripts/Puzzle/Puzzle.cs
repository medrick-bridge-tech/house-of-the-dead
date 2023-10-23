using System;
using UnityEngine;

public abstract class Puzzle : MonoBehaviour
{
    public Action OnPuzzleSolved, OnPuzzleFailed;
    
    public abstract void HandlePuzzleFinish();
}
