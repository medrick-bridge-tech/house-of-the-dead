using System;
using Cinemachine;
using UnityEngine;

public abstract class Puzzle : Interactable
{
    [SerializeField] public CinemachineVirtualCamera virtualCamera;
    public Action OnPuzzleSolved, OnPuzzleFailed;

    public abstract void HandlePuzzleFinish();

    protected virtual void Awake()
    {
        onInteraction.AddListener(HandleCharacterInteraction);
    }

    protected virtual void HandleCharacterInteraction(Character character)
    {
        character.PuzzleSolveMode(virtualCamera);
    }

}