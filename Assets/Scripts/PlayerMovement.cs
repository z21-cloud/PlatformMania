using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using PlatfromMania.Managers;

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

        [Header("Проверка земли")]
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundCheckRadius = 0.2f;
        [SerializeField] private LayerMask groundLayer;

        [Header("Скольжение по стене")]
        [SerializeField] private Transform wallCheck;
        [SerializeField] private Vector2 wallCheckSize = new Vector2(0.49f, 0.03f);
        [SerializeField] private LayerMask wallLayer;
        [SerializeField] private float wallSlideSpeed = 2f;

        [Header("Настройки гравитации")]
        [SerializeField] private float baseGravity = 2f;
        [SerializeField] private float maxFallSpeed = 18f;
        [SerializeField] private float fallSpeedMultiplier = 2f;

        private Rigidbody2D rb;
        private float movement;
        private bool isGrounded;
        private bool wasGrounded;
        private bool isWallSliding;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            jumpsRemaining = maxJumps;
        }

        void Update()
        {
            CheckGround();
            HandleGravity();
            HandleWallSlide();
            MoveHorizontal();
            Jump();
        }

        private void CheckGround()
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

            if (isGrounded && !wasGrounded)
            {
                jumpsRemaining = maxJumps;
            }

            wasGrounded = isGrounded;
        }

        private void HandleGravity()
        {
            if (rb.linearVelocityY < 0)
            {
                rb.gravityScale = baseGravity * fallSpeedMultiplier;
                rb.linearVelocity = new Vector2(rb.linearVelocityX, Mathf.Max(rb.linearVelocityY, -maxFallSpeed));
            }
            else
            {
                rb.gravityScale = baseGravity;
            }
        }

        private bool WallCheck() => (Physics2D.OverlapBox(wallCheck.position, wallCheckSize, 0, groundLayer));

        private void HandleWallSlide()
        {
            if(!isGrounded && WallCheck() && movement != 0)
            {
                isWallSliding = true;
                rb.linearVelocity = new Vector2(rb.linearVelocityX, Mathf.Max(rb.linearVelocityY, -wallSlideSpeed));
            }
            else
            {
                isWallSliding = false;
            }
        }

        private void MoveHorizontal()
        {
            movement = InputManager.Instance.GetHorizontalMovement();
            rb.linearVelocityX = movement * speed;
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

        private void OnDrawGizmosSelected()
        {
            if (groundCheck != null)
            {
                // Красный круг - проверка земли снизу
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
            }

            if (wallCheck != null)
            {
                // Синяя линия - проверка стены сбоку (вправо и влево)
                Gizmos.color = Color.blue;
                Gizmos.DrawWireCube(wallCheck.position, wallCheckSize);
            }
        }
    }
}
