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
            imageComponent = transform.GetChild(1).GetComponent<Image>();
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
            if (itemImage.sprite == null)
            {
                Color blackColor = Color.black;
                itemImage.color = blackColor;
            }
            
            // TODO: Reset the counter text
        }
    }
}