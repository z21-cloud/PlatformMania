using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using PlatfromMania.Managers;
using PlatfromMania.Services;
using PlatfromMania.Helpers;

namespace PlatfromMania.Core
{
    public class PlayerMovement : MonoBehaviour, IDirectionProvider
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
        [SerializeField] private int maxWallJumps = 4;
        private int wallJumpsRemaining;
        private bool isWallJumping;
        private float wallJumpTime = 0.5f;
        private float wallJumpTimer;

        [Header("Climbing")]
        [SerializeField] private float climbingSpeed = 3f;
        [SerializeField] private LayerMask climbingMask;

        [Header("Gravity Setup")]
        [SerializeField] private float baseGravity = 2f;
        [SerializeField] private float maxFallSpeed = 18f;
        [SerializeField] private float fallSpeedMultiplier = 2f;

        private GroundCheckService groundCheckService;
        private WallCheckService wallCheckService;
        private Rigidbody2D rb;

        private const float MOVEMENT_THRESHOLD = 0.01f;
        private const float VELOCITY_THRESHOLD = 0.1f;
        private const float JUMP_DELAY = 0.1f;

        private float horizontalMovement;
        
        private bool wasGrounded;
        private bool isWallSliding;
        private bool isTouchingWall;
        private bool isFalling;
        private bool isGrounded;
        private bool isClimbing;

        public float Direction => horizontalMovement;

        public event Action<bool> OnPlayerJumped;
        public event Action<bool> OnPlayerFalling;
        public event Action<bool> OnPlayerClimbing;

        private void Awake()
        {
            groundCheckService = new GroundCheckService();
            wallCheckService = new WallCheckService();
            rb = GetComponent<Rigidbody2D>();
            jumpsRemaining = maxJumps;
            wallJumpsRemaining = maxWallJumps;
        }

        void Update()
        {
            CheckEnvironment();
            HandlePhysics();
            HandleMovement();
            UpdateStates();
        }

        private void CheckEnvironment()
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
        }

        private void HandlePhysics()
        {
            HandleGravity();
            HandleWallSlide();
            ClimbingLadder();
        }

        private void HandleMovement()
        {
            if (isWallJumping) return;

            ProcessWallJump();
            ReadyToJump();
            Jump();
            MoveHorizontal();
        }

        private void UpdateStates()
        {
            Falling();
        }

        private void HandleGravity()
        {
            if (isWallSliding || isClimbing) return;

            if (rb.linearVelocityY < VELOCITY_THRESHOLD)
            {
                rb.gravityScale = baseGravity * fallSpeedMultiplier;
                rb.linearVelocity = new Vector2(rb.linearVelocityX, Mathf.Max(rb.linearVelocityY, -maxFallSpeed));
            }
            else
            {
                rb.gravityScale = baseGravity;
            }
        }

        private void HandleWallSlide()
        {
            if(!isGrounded && isTouchingWall && Mathf.Abs(horizontalMovement) > MOVEMENT_THRESHOLD)
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
                isWallJumping = false;
                wallJumpTimer = wallJumpTime;

                CancelInvoke(nameof(CancelWallJump));
            }
            else if (wallJumpTimer > 0)
            {
                wallJumpTimer -= Time.deltaTime;
            }
        }

        private void ReadyToJump()
        {
            if (isGrounded && !wasGrounded)
            {
                jumpsRemaining = maxJumps;
                wallJumpsRemaining = maxWallJumps;
            }

            wasGrounded = isGrounded;
        }

        private void Jump()
        {
            bool jumping = InputManager.Instance.GetJump();
            bool didJump = false;
            if (jumping && jumpsRemaining > 0)
            {
                rb.linearVelocityY = jumpHeight;
                jumpsRemaining--;
                didJump = true;
            }

            if(jumping && wallJumpTimer > 0 && wallJumpsRemaining > 0)
            {
                isWallJumping = true; //player jumped from the wall
                rb.linearVelocityY = jumpHeight;
                wallJumpTimer = 0;
                wallJumpsRemaining--;
                didJump = true;

                Invoke(nameof(CancelWallJump), wallJumpTime + JUMP_DELAY); //delay before next jump
            }

            if(didJump)
            {
                OnPlayerJumped?.Invoke(didJump);
            }
            else
            {
                OnPlayerJumped?.Invoke(didJump);
            }
        }

        private void Falling()
        {
            bool newValue = rb.linearVelocityY < VELOCITY_THRESHOLD &&
                            !isGrounded &&
                            !isTouchingWall &&
                            !isClimbing;

            if (newValue == isFalling) return;

            isFalling = newValue;
            OnPlayerFalling?.Invoke(isFalling);
        }

        private void ClimbingLadder()
        {
            float verticalMovement = InputManager.Instance.GetVerticalMovement();
            if (rb.IsTouchingLayers(climbingMask))
            {
                rb.gravityScale = 0;
                isClimbing = true;
                
                if (Mathf.Abs(verticalMovement) < MOVEMENT_THRESHOLD)
                {
                    rb.linearVelocityY = 0;
                    OnPlayerClimbing?.Invoke(false);
                }
                else
                {
                    rb.linearVelocityY = climbingSpeed * verticalMovement;
                    OnPlayerClimbing?.Invoke(true);
                }

                OnPlayerFalling?.Invoke(false);
            }
            else
            {
                if(isClimbing)
                {
                    rb.gravityScale = baseGravity;
                }

                OnPlayerClimbing?.Invoke(false);
                isClimbing = false;
            }
        }

        private void MoveHorizontal()
        {
            horizontalMovement = InputManager.Instance.GetHorizontalMovement();
            rb.linearVelocityX = horizontalMovement * speed;
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
