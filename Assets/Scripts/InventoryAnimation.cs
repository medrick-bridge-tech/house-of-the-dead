using DG.Tweening;
using UnityEngine;
public class InventoryAnimation : MonoBehaviour
{
    private bool isShowing = false;
    
    public void ShowHideInventory()
    {
        if (!isShowing)
        {
            isShowing = true;
            gameObject.transform.DOMove(new Vector3(transform.position.x,transform.position.y + 270,transform.position.z), .2f);
        }
        else
        {
            isShowing = false;
            gameObject.transform.DOMove(new Vector3(transform.position.x,transform.position.y - 270,transform.position.z), .2f);
        }
            
    }
}
