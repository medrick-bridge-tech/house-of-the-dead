using UnityEngine;

public class ItemPlace : MonoBehaviour 
{
    [SerializeField] string _itemName;
    [SerializeField] private InventoryUI _inventoryUI;
    [SerializeField] private LayerMask _layrMask;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layrMask))
            {
                if (_inventoryUI.SelectedItem == _itemName && hit.collider.gameObject == gameObject)
                {
                    gameObject.GetComponent<MeshRenderer>().enabled = true;
                }
            }
        }
    }
}
