using UnityEngine;

public class OfficeWindowPuzzle : MonoBehaviour
{
    [SerializeField] private GameObject exitButton;
    [SerializeField] private GameObject windowLock;
    
    private void EnableExitButton()
    {
        exitButton.SetActive(true);
    }

    private void DisableWindowLock()
    {
        windowLock.SetActive(false);
    }
}
