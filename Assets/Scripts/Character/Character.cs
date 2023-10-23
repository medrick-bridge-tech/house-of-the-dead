using UnityEngine;

public class Character : MonoBehaviour, InventorySystemCharacter
{
    private Inventory _inventory;

    public Inventory Inventory => _inventory;

    private void Awake()
    {
        _inventory = new Inventory();
    }
}