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
        [Header("Movement settings")]
        [SerializeField] private float speed = 5f;
        [SerializeField] private float jumpHeight = 5f;

        [Header("Jump settings")]
        [SerializeField] private int maxJumps = 2;
        private int jumpsRemaining;

        [Header("Ground check")]
        [SerializeField] private GroundCheck groundCheck;

        [Header("Wall sliding")]
        [SerializeField] private WallCheck wallCheck;
        [SerializeField] private float wallSlideSpeed = 2f;

        [Header("Wall Jumping")]
        [SerializeField] private Vector2 wallJumpPower = new Vector2(5f, 10f);
        private bool isWallJumping;
        private float wallJumpDirection;
        private float wallJumpTime = 0.5f;
        private float wallJumpTimer;

        private Rigidbody2D rb;
        private float movement;
        private bool wasGrounded;
        private bool isWallSliding;
        private bool isFalling;

        public event Action<bool> OnFallingChanged;
        public event Action<float> OnSpriteFliped;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            jumpsRemaining = maxJumps;
        }

        void Update()
        {
            HandleWallSlide();
            ProcessWallJump();
            ReadyToJump();
            Jump();
            Falling();

            if (isWallJumping) return;
            MoveHorizontal();
        }

        private void MoveHorizontal()
        {
            movement = InputManager.Instance.GetHorizontalMovement();
            OnSpriteFliped?.Invoke(movement);
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

            if(jumping && wallJumpTimer > 0f)
            {
                isWallJumping = true; //player jumped from the wall
                rb.linearVelocity = new Vector2(wallJumpDirection * wallJumpPower.x, wallJumpPower.y);
                wallJumpTimer = 0;

                if(transform.localScale.x != wallJumpDirection)
                {
                    OnSpriteFliped?.Invoke(wallJumpDirection);
                }

                Invoke(nameof(CancelWallJump), wallJumpTime + 0.1f); //delay before next jump
            }
        }

        private void ProcessWallJump()
        {
            if(isWallSliding)
            {
                isWallJumping = false; //update direction after jumping
                wallJumpDirection = -transform.localScale.x;
                wallJumpTimer = wallJumpTime;

                CancelInvoke(nameof(CancelWallJump));
            }
            else if(wallJumpTimer > 0f)
            {
                wallJumpTimer -= Time.deltaTime;
            }
        }

        private void Falling()
        {
            bool newValue = rb.linearVelocityY < 0 &&
                            !groundCheck.IsGrounded &&
                            !wallCheck.IsWallSliding;

            if (newValue == isFalling) return;

            isFalling = newValue;
            OnFallingChanged?.Invoke(newValue);
        }

        private void CancelWallJump()
        {
            isWallJumping = false;
        }
    }
}
