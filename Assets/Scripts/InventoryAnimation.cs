using DG.Tweening;
using UnityEngine;
public class InventoryAnimation : MonoBehaviour
{
    private RectTransform _rectTransform;
    private bool _isShowing = false;
    public bool isMagnifierSelected { get; set; }
    
    [SerializeField] private Animator magnifierAnim;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }
    
    public void ShowHideInventory()
    {
        if (!_isShowing)
        {
            _isShowing = true;
            _rectTransform.DOAnchorPos(_rectTransform.anchoredPosition + Vector2.up * 138, .2f);
        }
        else
        {
            _isShowing = false;
            _rectTransform.DOAnchorPos(_rectTransform.anchoredPosition + Vector2.down * 138, .2f);
        }
    }

    public void MagnifierSelected()
    {
        if (isMagnifierSelected)
        {
            isMagnifierSelected = false;
            magnifierAnim.SetBool("Selected", false);
        }
        else
        {
            isMagnifierSelected = true;
            magnifierAnim.SetBool("Selected", true);
        }
    }
}
