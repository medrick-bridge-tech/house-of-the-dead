using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class ItemPresenter : MonoBehaviour
    {
        [SerializeField] private Image itemImage;
        //[SerializeField] private Text amountText;

        private string itemName;

        public string ItemName => itemName;

        public void Setup(string itemName, Sprite image/*, int amount = 1*/)
        {
            this.itemName = itemName;
            itemImage.sprite = image;
            // amountText.text = amount.ToString();
        }

        public void Clear()
        {
            itemName = "";
            itemImage = null;
            
            // TODO: Reset the image
            // TODO: Reset the counter text
        }
    }
}