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

        private void Start()
        {
            animationContoroller = GetComponent<AnimationContoroller>();
            animationContoroller.SetState(CharacterState.Idle);
            SetDirection(Vector2.down);
        }

        private void Update()
        {
            Move();
            SetDirection();
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
                    _moving = false;
                }
            }
            else
            {
                animationContoroller.SetState(CharacterState.Run);
                transform.position += (Vector3)direction.normalized * MovementSpeed * Time.deltaTime;
                _moving = true;
            }
        }

        private void SetDirection()
        {
            Vector2 direction;

            if (Input.GetKeyDown(KeyCode.A))
            {
                direction = Vector2.left;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                direction = Vector2.right;
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                direction = Vector2.up;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                direction = Vector2.down;
            }
            else return;

            SetDirection(direction);
        }

        private void SetDirection(Vector2 direction)
        {
            if (Direction == direction) return;

            Direction = direction;


            int index;

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
            else
            {
                throw new NotSupportedException();
            }

            for (var i = 0; i < character.Count; i++)
            {
                character[i].gameObject.SetActive(i == index);
            }
        }
    }
}