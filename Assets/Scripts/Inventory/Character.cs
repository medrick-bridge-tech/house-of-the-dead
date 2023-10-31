using Cinemachine;
using UnityEngine;

public class Character : MonoBehaviour, InventorySystemCharacter
{
    [SerializeField] private GameObject joystick, backButton, openButton;
    
    private CameraSwitcher cameraSwitcher;
    private Inventory _inventory;

    public Inventory Inventory => _inventory;

    private void Awake()
    {
        _inventory = new Inventory();
        cameraSwitcher = GetComponent<CameraSwitcher>();
    }

    public void PuzzleSolveMode(CinemachineVirtualCamera virtualCamera)
    {
        cameraSwitcher.SwitchToPuzzleCamera(virtualCamera);
        joystick.SetActive(false);
        backButton.SetActive(true);
    }
    
    public void DefocusFromPuzzle()
    {
        cameraSwitcher.DefocusPuzzleCamera();
        joystick.SetActive(true);
        backButton.SetActive(false);
        openButton.SetActive(false);
    }

}