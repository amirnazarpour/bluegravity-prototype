using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Bluegravity.Character
{
    [RequireComponent(typeof(Animator))]
    public class AnimationContoroller : MonoBehaviour
    {
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void SetState(CharacterState state)
        {
            animator.SetInteger("State", (int)state);
        }
    }
}