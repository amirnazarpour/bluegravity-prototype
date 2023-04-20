using UnityEngine;
using System;

using UnityEngine.UI;

namespace Bluegravity.Shop
{
    [RequireComponent(typeof(Button))]
    public class TabItem : MonoBehaviour
    {
        public int id;

        private Button button;
        public Action<TabItem> OnClick;
        public GameObject selectImage;

        public Image icon;
        

        public void Init(int id, Sprite icon)
        {
            this.id = id;
            this.icon.sprite = icon;
            
            button = GetComponent<Button>();
            button.onClick.AddListener(Select);
            selectImage.SetActive(false);
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