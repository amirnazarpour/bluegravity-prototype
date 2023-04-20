using System.Collections;
using System.Collections.Generic;
using Bluegravity.Common;
using Igloovy.Patterns.Pool;
using UnityEngine;

namespace Bluegravity.Shop
{
    public class ShopManager : MonoBehaviour
    {
        public List<ChangeablePart> tabData;
        public Transform root;
        public TabItem item;
        private TabItem activeTabItem;

        public Pool<ShopItem> shopItems;

        private ShopItem activeShopItem;

        private void Start()
        {
            for (int i = 0; i < tabData.Count; i++)
            {
                TabItem tab = Instantiate(item, root);
                tab.Init(i, tabData[i].spriteGroup.icon);
                tab.OnClick += TabSelected;
                if (i != 0) continue;
                activeTabItem = tab;
                TabSelected(tab);
            }
        }

        public void TabSelected(TabItem item)
        {
            shopItems.TurnOffItems();
            activeTabItem.Exit();
            activeTabItem = item;
            activeTabItem.Enter();

            int id = item.id;

            for (var i = 0; i < tabData[id].spriteGroup.SpriteData.Count; i++)
            {
                var data = tabData[id].spriteGroup.SpriteData[i];
                var shopItem = shopItems.GetActive.Init(i, data.icon, data.price);
                shopItem.OnClick += Selected;
                if (i != 0) continue;
                activeShopItem = shopItem;
                Selected(shopItem);
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
    }
}