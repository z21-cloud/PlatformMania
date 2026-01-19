using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlatfromMania.Managers;

namespace PlatfromMania.Core
{
    public class PlayerController : MonoBehaviour
    {
        private float movement = 0;
        private bool isJumping = false;
        private bool isShooting = false;
        private bool isFacingRight = true;

        void Start()
        {

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
            if(isFacingRight && movement < 0 || !isFacingRight && movement > 0)
            {
                isFacingRight = !isFacingRight;
                Vector3 ls = transform.localScale;
                ls.x *= -1f;
                transform.localScale = ls;
            }
        }

        private void HandleInput()
        {
            movement = InputManager.Instance.GetHorizontalMovement();
            isJumping = InputManager.Instance.GetJump();
            isShooting = InputManager.Instance.GetMouseButton();
        }
    }
}

