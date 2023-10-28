using UnityEngine;

public class InventoryMock : MonoBehaviour , ISelectedItem
{
    public string SelectedItem { get; private set; } = "nothing";
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            SelectedItem = "Map";
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            SelectedItem = "Fuse";
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            SelectedItem = "Fuse";
        }
        else
        {
            SelectedItem = "nothing";
        }
    }
}
