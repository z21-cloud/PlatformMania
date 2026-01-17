using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

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
        [SerializeField] private LayerMask groundMask;

        [Header("Скольжение по стене")]
        [SerializeField] private Transform wallCheck;
        [SerializeField] private float wallCheckDistance = 0.3f;
        [SerializeField] private float wallSlideSpeed = 2f;

        private Rigidbody2D rb;
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
            CheckWallSlide();
            MoveHorizontal();
            Jump();
            ApplyWallSlide();
        }

        private void CheckGround()
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundMask);

            if(isGrounded && !wasGrounded)
            {
                jumpsRemaining = maxJumps;
            }

            wasGrounded = isGrounded;
        }

        private void CheckWallSlide()
        {
            float movement = InputManager.Instance.GetHorizontalMovement();

            if (!isGrounded && movement != 0 && rb.linearVelocityY == 0)
            {
                Vector2 direction = new Vector2(Mathf.Sin(movement), 0);
                RaycastHit2D hit = Physics2D.Raycast(wallCheck.position, direction, wallCheckDistance, groundMask);
                Debug.Log(hit.collider.name);
                isWallSliding = hit.collider != null;
            }
            else
            {
                isWallSliding = false;
            }
        }

        private void MoveHorizontal()
        {
            float movement = InputManager.Instance.GetHorizontalMovement();
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

        private void ApplyWallSlide()
        {
            if(isWallSliding)
            {
                rb.linearVelocityY = Math.Max(rb.linearVelocityY, -jumpHeight / wallSlideSpeed);
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
                Gizmos.DrawLine(wallCheck.position, wallCheck.position + Vector3.right * wallCheckDistance);
                Gizmos.DrawLine(wallCheck.position, wallCheck.position + Vector3.left * wallCheckDistance);
            }
        }
    }
}
