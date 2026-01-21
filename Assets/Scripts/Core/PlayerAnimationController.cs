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
        private float yVelocity = 0f;
        private bool isShooting = false;

        private void OnEnable()
        {
            playerMovement.OnYVelocityChanged += ChangeYVelocity;
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
            if (yVelocity > 0)
                anim.SetFloat("yVelocity", 1);
            else
                anim.SetFloat("yVelocity", 0);
        }

        private void FallingAnimation()
        {
            if (yVelocity < 0)
                anim.SetFloat("yVelocity", -1);
            else
                anim.SetFloat("yVelocity", 0);
        }

        private void ChangeYVelocity(float value)
        {
            yVelocity = value;
        }

        private void HandleInput()
        {
            movement = InputManager.Instance.GetHorizontalMovement();
            isShooting = InputManager.Instance.GetMouseButton();
        }

        private void OnDisable()
        {
            playerMovement.OnYVelocityChanged -= ChangeYVelocity;
        }
    }
}

