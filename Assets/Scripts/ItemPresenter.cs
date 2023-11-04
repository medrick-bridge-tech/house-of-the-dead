using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class ItemPresenter : MonoBehaviour
    {
        [SerializeField] private Image itemImage;
        // TODO: [SerializeField] private Text amountText;

        private string itemName;
        public Image imageComponent;

        public string ItemName => itemName;

        private void Start()
        {
            imageComponent = GetComponent<Image>();
        }

        public void Setup(string itemName, Sprite image)
        {
            this.itemName = itemName;
            itemImage.sprite = image;
            // TODO: amountText.text =.;
            
        }

        public void Clear()
        {
            itemName = "";
            itemImage.sprite = null;
            // TODO: Reset the counter text
        }
    }
}