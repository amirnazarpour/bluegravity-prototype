using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bluegravity.Character
{
    public class Controller : MonoBehaviour
    {
        public AnimationContoroller animationContoroller;
        public List<GameObject> character;
        public Vector2 Direction { get; private set; }

        public int MovementSpeed;

        private bool _moving;

        public Action OnShop;

        public bool shopActive;

        public GameObject shopSing;

   

        private void Start()
        {
            animationContoroller = GetComponent<AnimationContoroller>();
            animationContoroller.SetState(CharacterState.Idle);
        }

        private void Update()
        {
            Move();
            if (shopActive)
            {
                if (Input.GetKey(KeyCode.R))
                {
                    OnShop?.Invoke();
                }
            }
        }

        private void Move()
        {
            if (MovementSpeed == 0) return;

            var direction = Vector2.zero;

            if (Input.GetKey(KeyCode.A))
            {
                direction += Vector2.left;
            }

            if (Input.GetKey(KeyCode.D))
            {
                direction += Vector2.right;
            }

            if (Input.GetKey(KeyCode.W))
            {
                direction += Vector2.up;
            }

            if (Input.GetKey(KeyCode.S))
            {
                direction += Vector2.down;
            }

            if (direction == Vector2.zero)
            {
                if (_moving)
                {
                    animationContoroller.SetState(CharacterState.Idle);
                    SetDirection(direction);
                    _moving = false;
                }
            }
            else
            {
                SetDirection(direction);
                animationContoroller.SetState(CharacterState.Run);
                transform.position += (Vector3)direction.normalized * MovementSpeed * Time.deltaTime;
                _moving = true;
            }
        }


        private void SetDirection(Vector2 direction)
        {
            if (Direction == direction) return;

            Direction = direction;


            int index = 5;

            if (direction == Vector2.left)
            {
                index = 2;
            }
            else if (direction == Vector2.right)
            {
                index = 3;
            }
            else if (direction == Vector2.up)
            {
                index = 1;
            }
            else if (direction == Vector2.down)
            {
                index = 0;
            }

            if (index == 5)
            {
                return;
            }

            for (var i = 0; i < character.Count; i++)
            {
                character[i].gameObject.SetActive(i == index);
            }
        }
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Shop"))
            {
                shopActive = true;
                shopSing.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Shop"))
            {
                shopActive = false;
                shopSing.SetActive(false);
            }
        }
    }
}