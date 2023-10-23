using UnityEngine;

public class InventoryItem : Interactable
    {
        [SerializeField] private ItemData itemData;

        private void Awake()
        {
            onInteraction.AddListener(AddToInventory);
        }

        private void AddToInventory(InventorySystemCharacter interactor)
        {
            var inventory = interactor.Inventory;
            if (inventory != null)
                inventory.AddItem(itemData);
        }
    }