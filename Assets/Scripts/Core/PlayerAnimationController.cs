using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlatfromMania.Managers;

namespace PlatfromMania.Core
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [Header("Player Movement Checker")]
        [SerializeField] private PlayerMovement playerMovement;

        private Animator anim;
        private float movement = 0f;
        private bool isJumping = false;
        private bool isShooting = false;
        private bool isFalling = false;

        private void OnEnable()
        {
            playerMovement.OnFallingChanged += SetFallValue;
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

        private void SetFallValue(bool value)
        {
            isFalling = value;
        }

        private void HandleInput()
        {
            movement = InputManager.Instance.GetHorizontalMovement();
            isJumping = InputManager.Instance.GetJump();
            isShooting = InputManager.Instance.GetMouseButton();
        }

        private void OnDisable()
        {
            playerMovement.OnFallingChanged -= SetFallValue;
        }
    }
}

