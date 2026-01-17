using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlatfromMania.Core;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private float movement = 0;
    private bool isJumping = false;
    private bool isShooting = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        movement = InputManager.Instance.GetHorizontalMovement();
        isJumping = InputManager.Instance.GetJump();
        isShooting = InputManager.Instance.GetMouseButton();
    }
}
