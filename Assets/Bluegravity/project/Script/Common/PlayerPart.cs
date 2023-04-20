using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bluegravity.Common
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerPart : MonoBehaviour
    {
        public string name;
        [HideInInspector] public SpriteRenderer sprite;
    }
}