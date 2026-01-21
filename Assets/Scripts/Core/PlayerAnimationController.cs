using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlatfromMania.Managers;
using System;

namespace PlatfromMania.Core
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [Header("Player Movement Checker")]
        [SerializeField] private PlayerMovement playerMovement;

        private Animator anim;
        private float horizontalMovement = 0f;
        private bool isShooting = false;
        private bool isJumping = false;
        private bool isFalling = false;
        private bool isClimbing = false;


        private void OnEnable()
        {
            playerMovement.OnPlayerFalling += Falling;
            playerMovement.OnPlayerJumped += Jumping;
            playerMovement.OnPlayerClimbing += Climbing;
        }

        void Start()
        {
            anim = GetComponent<Animator>();
        }

        private void Falling(bool value)
        {
            isFalling = value;
        }

        private void Jumping(bool value)
        {
            isJumping = value;
        }

        private void Climbing(bool value)
        {
            isClimbing = value;
        }

        // Update is called once per frame
        void Update()
        {
            HandleInput();
            MovementAnimation();
            JumpAnimation();
            FallingAnimation();
            ClimbingAnimation();
        }

        private void HandleInput()
        {
            horizontalMovement = InputManager.Instance.GetHorizontalMovement();
            isShooting = InputManager.Instance.GetMouseButton();
        }

        private void MovementAnimation()
        {
            if (horizontalMovement != 0) anim.SetBool("isMoving", true);
            else anim.SetBool("isMoving", false);
        }

        private void JumpAnimation()
        {
            if (isJumping) anim.SetBool("isJumping", true);
            else anim.SetBool("isJumping", false);
        }

        private void FallingAnimation()
        {
            if (isFalling) anim.SetBool("isFalling", true);
            else anim.SetBool("isFalling", false);
        }

        private void ClimbingAnimation()
        {
            if (isClimbing) anim.SetBool("isClimbing", true);
            else anim.SetBool("isClimbing", false);
        }

        private void OnDisable()
        {
            playerMovement.OnPlayerFalling -= Falling;
            playerMovement.OnPlayerJumped -= Jumping;
            playerMovement.OnPlayerClimbing -= Climbing;
        }
    }
}

