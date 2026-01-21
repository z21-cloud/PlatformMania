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
        private float movement = 0f;
        private bool isShooting = false;
        private bool isJumping = false;
        private bool isFalling = false;


        private void OnEnable()
        {
            playerMovement.OnPlayerFalling += Falling;
            playerMovement.OnPlayerJumped += Jumping;
        }

        private void Jumping(bool value)
        {
            isJumping = value;
        }

        private void Falling(bool value)
        {
            isFalling = value;
        }

        void Start()
        {
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            HandleInput();
            MovementAnimation();
            JumpAnimation();
            FallingAnimation();
        }

        private void MovementAnimation()
        {
            if (movement != 0)
                anim.SetBool("isMoving", true);
            else
                anim.SetBool("isMoving", false);
        }

        private void JumpAnimation()
        {
            if (isJumping)
                anim.SetBool("isJumping", true);
            else
                anim.SetBool("isJumping", false);
        }

        private void FallingAnimation()
        {
            if (isFalling)
                anim.SetBool("isFalling", true);
            else
                anim.SetBool("isFalling", false);
        }

        private void HandleInput()
        {
            movement = InputManager.Instance.GetHorizontalMovement();
            isShooting = InputManager.Instance.GetMouseButton();
        }

        private void OnDisable()
        {
            playerMovement.OnPlayerFalling -= Falling;
            playerMovement.OnPlayerJumped -= Jumping;
        }
    }
}

