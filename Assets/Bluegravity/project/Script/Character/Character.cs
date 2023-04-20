using System.Collections.Generic;
using System.Linq;
using Bluegravity.Common;
using UnityEngine;

namespace Bluegravity.Character
{
    public class Character : MonoBehaviour
    {
        public List<ChangeablePart> changeablePart;

        public void SetPart()
        {
            foreach (var changeable in changeablePart)
            {
                string name = PlayerPrefs.GetString(changeable.name);

                if (name != string.Empty)
                {
                    foreach (var playerPart in changeable.playerPart)
                    {
                        var data = changeable.spriteGroup.SpriteData.First(x => x.name == name);
                        foreach (var sprite in data.Sprites)
                        {
                            if (playerPart.name == sprite.name)
                            {
                                playerPart.sprite.sprite = sprite;
                            }
                        }
                    }
                }
                else
                {
                    foreach (var playerPart in changeable.playerPart)
                    {
                        var data = changeable.spriteGroup.SpriteData[0];
                        foreach (var sprite in data.Sprites)
                        {
                            if (playerPart.name == sprite.name)
                            {
                                playerPart.sprite.sprite = sprite;
                            }
                        }
                    }
                }

            }
        }
    }
}