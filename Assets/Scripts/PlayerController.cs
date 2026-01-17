using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PlatfromMania.Core
{
    public class PlayerController : MonoBehaviour
    {
        private float movement = 0;
        private bool isJumping = false;
        private bool isShooting = false;
        private SpriteRenderer characterSprite;
        void Start()
        {
            characterSprite = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            HandleInput();
            UpdateSpriteFlip();

            /*
             * if movement == 0 => speed = 0
             * if isJumping == false => not jump
             * if isShooting == false => don't shoot
             */
        }

        private void UpdateSpriteFlip()
        {
            if (movement == 0) return;
            characterSprite.flipX = movement < 0;
        }

        private void HandleInput()
        {
            movement = InputManager.Instance.GetHorizontalMovement();
            isJumping = InputManager.Instance.GetJump();
            isShooting = InputManager.Instance.GetMouseButton();
        }
    }
}

