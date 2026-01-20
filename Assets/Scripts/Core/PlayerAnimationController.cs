using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlatfromMania.Managers;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator anim;
    private float movement = 0f;
    private bool isJumping = false;
    private bool isShooting = false;

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

    }

    private void HandleInput()
    {
        movement = InputManager.Instance.GetHorizontalMovement();
        isJumping = InputManager.Instance.GetJump();
        isShooting = InputManager.Instance.GetMouseButton();
    }
}
