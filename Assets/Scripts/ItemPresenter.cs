using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class ItemPresenter : MonoBehaviour
    {
        [SerializeField] private Image itemImage;
        // TODO: [SerializeField] private Text amountText;

        private string itemName;

        public string ItemName => itemName;

        public void Setup(string itemName, Sprite image)
        {
            this.itemName = itemName;
            itemImage.sprite = image;
            // TODO: amountText.text = amount.ToString();
        }

        public void Clear()
        {
            itemName = "";
            itemImage.sprite = null;
            // TODO: Reset the counter text
        }
    }
}