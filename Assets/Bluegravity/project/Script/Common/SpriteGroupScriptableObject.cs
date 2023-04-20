using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Bluegravity.Common
{
    [CreateAssetMenu(fileName = "SpriteGroup", menuName = "ScriptableObjects/SpriteGroup")]
    public class SpriteGroupScriptableObject : ScriptableObject
    {
        public string name;
        public Sprite icon;
        public List<SpriteData> SpriteData;
        
        public List<string> searchName;
    
        [ContextMenu("checkValid")]
        public void checkValid()
        {
            foreach (var spriteItem in SpriteData)
            {
                List<Sprite> newSprites = new List<Sprite>();
                foreach (var item in spriteItem.Sprites)
                {
                    foreach (var search in searchName)
                    {
                        if (item.name.Contains(search))
                        {
                            newSprites.Add(item);
                        }
                    }
                }
                spriteItem.Sprites = newSprites;
            }
        }
    }
}
