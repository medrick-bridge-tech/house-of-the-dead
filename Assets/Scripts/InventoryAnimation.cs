using DG.Tweening;
using UnityEngine;
public class InventoryAnimation : MonoBehaviour
{
    private RectTransform _rectTransform;
    private bool _isShowing = false;
    public bool IsMagnifierSelected { get; set; }
    
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
        if (IsMagnifierSelected)
        {
            IsMagnifierSelected = false;
            magnifierAnim.SetBool("Selected", false);
        }
        else
        {
            IsMagnifierSelected = true;
            magnifierAnim.SetBool("Selected", true);
        }
    }
}
