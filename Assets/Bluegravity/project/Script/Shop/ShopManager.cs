using System;
using System.Collections.Generic;
using System.Linq;
using Bluegravity.Common;
using Emaj.Patterns;
using UnityEngine;

namespace Bluegravity.Shop
{
    public class ShopManager : MonoBehaviour
    {
        public List<ChangeablePart> tabData;
        public Transform root;
        public TabItem item;
        public Pool<ShopItem> shopItems;
        public Action OnClose;

        private TabItem activeTabItem;
        private ShopItem activeShopItem;

        private void Start()
        {
            TabItem first = null;
            for (int i = 0; i < tabData.Count; i++)
            {
                TabItem tab = Instantiate(item, root);
                tab.Init(i, tabData[i].spriteGroup.icon);
                tab.OnClick += TabSelected;
                activeTabItem = tab;
                TabSelected(tab);
                activeTabItem.Exit();
                if (i == 0)
                {
                    first = tab;
                }
            }
            TabSelected(first);
        }

        public void TabSelected(TabItem item)
        {
            shopItems.TurnOffItems();
            activeTabItem.Exit();
            activeTabItem = item;
            activeTabItem.Enter();

            int id = item.id;

            string name = PlayerPrefs.GetString(tabData[id].name);


            for (var i = 0; i < tabData[id].spriteGroup.SpriteData.Count; i++)
            {
                var data = tabData[id].spriteGroup.SpriteData[i];
                var shopItem = shopItems.GetActive.Init(i, data.icon, data.price);
                shopItem.OnClick += Selected;
                if (name == string.Empty)
                {
                    if (i == 0)
                    {
                        activeShopItem = shopItem;
                        Selected(shopItem);
                    }
                }
                else
                {
                    if (data.name == name)
                    {
                        foreach (var playerPart in tabData[id].playerPart)
                        {
                            var spriteData = tabData[id].spriteGroup.SpriteData.First(x => x.name == name);
                            foreach (var sprite in spriteData.Sprites)
                            {
                                string partName = playerPart.name;
                                if (partName.Replace("Right", "Left") == sprite.name)
                                {
                                    playerPart.sprite.sprite = sprite;
                                }
                            }
                        }

                        shopItem.Enter();
                        activeShopItem = shopItem;
                        Selected(shopItem);
                    }
                }
            }
        }

        public void Selected(ShopItem item)
        {
            activeShopItem.Exit();
            activeShopItem = item;
            activeShopItem.Enter();
            int id = item.id;

            PlayerPrefs.SetString(tabData[activeTabItem.id].name,
                tabData[activeTabItem.id].spriteGroup.SpriteData[id].name);
            PlayerPrefs.Save();
            foreach (var playerPart in tabData[activeTabItem.id].playerPart)
            {
                foreach (var spriteData in tabData[activeTabItem.id].spriteGroup.SpriteData[id].Sprites)
                {
                    if (playerPart.name == spriteData.name)
                    {
                        Debug.Log(playerPart.name);
                        playerPart.sprite.sprite = spriteData;
                        break;
                    }
                }
            }
        }


        public void Close()
        {
            OnClose?.Invoke();
            gameObject.SetActive(false);
        }
    }
}