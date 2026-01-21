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
        [SerializeField] private Transform groundCheckTransform;
        [SerializeField] private float groundCheckRadius = 0.2f;
        [SerializeField] private LayerMask groundLayer;

        [Header("Wall sliding")]
        [SerializeField] private Transform wallCheckTransform;
        [SerializeField] private Vector2 wallCheckSize = new Vector2(0.49f, 0.03f);
        [SerializeField] private LayerMask wallLayer;
        [SerializeField] private float wallSlideSpeed = 2f;

        [Header("Wall Jumping")]
        [SerializeField] private Vector2 wallJumpPower = new Vector2(5f, 10f);
        private bool isWallJumping;
        private float wallJumpDirection;
        private float wallJumpTime = 0.5f;
        private float wallJumpTimer;

        private GroundCheckService groundCheckService;
        private WallCheckService wallCheckService;
        private Rigidbody2D rb;
        private float movement;
        private bool wasGrounded;
        private bool isWallSliding;
        private bool isTouchingWall;
        private bool isFalling;
        private bool isGrounded;

        public event Action<float> OnSpriteFliped;
        public event Action<bool> OnPlayerJumped;
        public event Action<bool> OnPlayerFalling;

        private void Start()
        {
            groundCheckService = new GroundCheckService();
            wallCheckService = new WallCheckService();
            rb = GetComponent<Rigidbody2D>();
            jumpsRemaining = maxJumps;
        }

        void Update()
        {
            isGrounded = groundCheckService.Check(
                groundCheckTransform.position, 
                groundCheckRadius, 
                groundLayer);

            isTouchingWall = wallCheckService.Check(
                wallCheckTransform.position, 
                wallCheckSize, 
                0, 
                wallLayer);

            HandleWallSlide();
            ProcessWallJump();
            ReadyToJump();
            Jump();
            Falling();

            if (isWallJumping) return;
            MoveHorizontal();
        }

        private void HandleWallSlide()
        {
            if(!isGrounded && isTouchingWall && movement != Mathf.Epsilon)
            {
                isWallSliding = true;
                rb.linearVelocity = new Vector2(rb.linearVelocityX, Mathf.Max(rb.linearVelocityY, -wallSlideSpeed));
            }
            else
            {
                isWallSliding = false;
            }
        }

        private void ProcessWallJump()
        {
            if (isWallSliding)
            {
                isWallJumping = false; //update direction after jumping
                wallJumpDirection = -transform.localScale.x;
                wallJumpTimer = wallJumpTime;

                CancelInvoke(nameof(CancelWallJump));
            }
            else if (wallJumpTimer > Mathf.Epsilon)
            {
                wallJumpTimer -= Time.deltaTime;
            }
        }

        private void ReadyToJump()
        {
            if (isGrounded && !wasGrounded)
            {
                jumpsRemaining = maxJumps;
            }

            wasGrounded = isGrounded;
        }

        private void Jump()
        {
            bool jumping = InputManager.Instance.GetJump();
            if (jumping && jumpsRemaining > Mathf.Epsilon)
            {
                rb.linearVelocityY = jumpHeight;
                jumpsRemaining--;
                OnPlayerJumped.Invoke(jumping);
            }

            if(jumping && wallJumpTimer > Mathf.Epsilon)
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

            OnPlayerJumped?.Invoke(jumping);
        }

        private void Falling()
        {
            bool newValue = rb.linearVelocityY < Mathf.Epsilon &&
                            !isGrounded &&
                            !isTouchingWall;

            if (newValue == isFalling) return;

            isFalling = newValue;
            OnPlayerFalling?.Invoke(isFalling);
        }

        private void MoveHorizontal()
        {
            movement = InputManager.Instance.GetHorizontalMovement();
            OnSpriteFliped?.Invoke(movement);
            rb.linearVelocityX = movement * speed;
        }

        private void CancelWallJump()
        {
            isWallJumping = false;
        }

        private void OnDrawGizmos()
        {
            GroundCheckService.DrawDebugCircle(
                groundCheckTransform.position,
                groundCheckRadius,
                isGrounded ? Color.green : Color.red);

            WallCheckService.DrawDebugCircle(
                wallCheckTransform.position,
                wallCheckSize,
                isTouchingWall ? Color.blue : Color.red);
        }
    }
}
