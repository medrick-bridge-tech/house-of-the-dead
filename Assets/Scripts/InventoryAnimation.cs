using DG.Tweening;
using UnityEngine;
public class InventoryAnimation : MonoBehaviour
{
    private RectTransform _rectTransform;
    private bool isShowing = false;


    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }
    
    public void ShowHideInventory()
    {
        if (!isShowing)
        {
            isShowing = true;
            _rectTransform.DOAnchorPos(_rectTransform.anchoredPosition + Vector2.up * 138, .2f);
        }
        else
        {
            isShowing = false;
            _rectTransform.DOAnchorPos(_rectTransform.anchoredPosition + Vector2.down * 138, .2f);
        }
            
    }
}
