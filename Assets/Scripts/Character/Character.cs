using UnityEngine;

public class Character : MonoBehaviour, InventorySystemCharacter
{
    private Inventory _inventory;

    public Inventory Inventory
    {
        get
        {
            var inventory = _inventory;
            return inventory;
        }
    }

    private void Awake()
    {
        _inventory = new Inventory();
    }
}