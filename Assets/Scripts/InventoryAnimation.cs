using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryAnimation : MonoBehaviour
{
    [SerializeField] private Animator inventoryAnim;
    private bool isShowing = false;
    
    public void ShowHideInventory()
    {
        if (!isShowing)
        {
            isShowing = true;
            inventoryAnim.SetBool("Show",true);
        }
        else
        {
            isShowing = false;
            inventoryAnim.SetBool("Show", false);
        }
            
    }
}
