using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using PlatfromMania.Managers;
using PlatfromMania.Services;

namespace PlatfromMania.Core
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Настройки движения")]
        [SerializeField] private float speed = 5f;
        [SerializeField] private float jumpHeight = 5f;

        [Header("Настройки прыжка")]
        [SerializeField] private int maxJumps = 2;
        private int jumpsRemaining;

        [Header("Ground check")]
        [SerializeField] private GroundCheck groundCheck;

        [Header("Wall sliding")]
        [SerializeField] private WallCheck wallCheck;
        [SerializeField] private float wallSlideSpeed = 2f;

        private Rigidbody2D rb;
        private float movement;
        private bool wasGrounded;
        private bool isWallSliding;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            jumpsRemaining = maxJumps;
        }

        void Update()
        {
            MoveHorizontal();
            HandleWallSlide();
            ReadyToJump();
            Jump();
        }

        private void MoveHorizontal()
        {
            movement = InputManager.Instance.GetHorizontalMovement();
            rb.linearVelocityX = movement * speed;
        }

        private void HandleWallSlide()
        {
            if(!groundCheck.IsGrounded && wallCheck.IsWallSliding && movement != 0)
            {
                isWallSliding = true;
                rb.linearVelocity = new Vector2(rb.linearVelocityX, Mathf.Max(rb.linearVelocityY, -wallSlideSpeed));
            }
            else
            {
                isWallSliding = false;
            }
        }

        private void ReadyToJump()
        {
            if (groundCheck.IsGrounded && !wasGrounded)
            {
                jumpsRemaining = maxJumps;
            }

            wasGrounded = groundCheck.IsGrounded;
        }

        private void Jump()
        {
            bool jumping = InputManager.Instance.GetJump();
            if (jumping && jumpsRemaining > 0)
            {
                rb.linearVelocityY = jumpHeight;
                jumpsRemaining--;
            }
        }
    }
}
