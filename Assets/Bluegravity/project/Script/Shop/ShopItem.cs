using UnityEngine;
using UnityEngine.UI;
using System;

namespace Bluegravity.Shop
{
    public class ShopItem : MonoBehaviour
    {
        public int id;
        public Image icon;
        public GameObject selectImage;
        public Text txtPrice;
        public Button button;

        public Action<ShopItem> OnClick;

        public ShopItem Init(int id, Sprite icon, int price)
        {
            this.id = id;
            this.icon.sprite = icon;
            txtPrice.text = price.ToString();
            button = GetComponent<Button>();
            button.onClick.AddListener(Select);
            selectImage.SetActive(false);
            return this;
        }

        public void Enter()
        {
            selectImage.SetActive(true);
        }

        private void Select()
        {
            OnClick?.Invoke(this);
        }

        public void Exit()
        {
            selectImage.SetActive(false);
        }
    }
}