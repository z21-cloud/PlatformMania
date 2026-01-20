using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlatfromMania.Managers;
using PlatfromMania.Core;
using PlatfromMania.Services;

public class PlayerAnimationController : MonoBehaviour
{
    [Header("Checkers")]
    [SerializeField] private GroundCheck groundCheck;
    [SerializeField] private WallCheck wallCheck;
    private Animator anim;
    private float movement = 0f;
    private bool isJumping = false;
    private bool isShooting = false;
    private bool isFalling = false;

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
        //isFalling = !groundCheck.IsGrounded && !wallCheck.IsWallSliding;
        if (isFalling)
            anim.SetBool("isFalling", true);
        else
            anim.SetBool("isFalling", false);
    }

    public void SetFallValue(bool value)
    {
        isFalling = value;
    }

    private void HandleInput()
    {
        movement = InputManager.Instance.GetHorizontalMovement();
        isJumping = InputManager.Instance.GetJump();
        isShooting = InputManager.Instance.GetMouseButton();
    }
}
