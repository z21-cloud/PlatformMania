using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlatfromMania.Services;
using PlatfromMania.Core;

public class GooberMovement : MonoBehaviour, IDirectionProvider
{
    [Header("Goober Movement Setup")]
    [SerializeField] private Vector2 moveTrajectory = new Vector2(2f, 2f);
    [SerializeField] private float timeBetweenSteps = 0.25f;

    [Header("Ground Check Setup")]
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private float groundCheckDistance = 0.5f;
    [SerializeField] private LayerMask groundMask;

    private float timer = 0;
    private Rigidbody2D rb;
    private float direction = 1f;
    private bool isGrounded;
    private GroundCheckService groundCheck;

    public float Direction => direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheck = new GroundCheckService();
    }

    private void Update()
    {
        isGrounded = groundCheck.CheckRaycast(
                groundCheckTransform.position,
                Vector2.down,
                groundCheckDistance,
                groundMask);

        timer += Time.deltaTime;
        if (timer >= timeBetweenSteps)
        {
            Patrol();
            timer = 0;
        }
    }

    private void Patrol()
    {
        if (!isGrounded)
        {
            direction *= -1f;
        }

        Jump(direction);
    }

    private void Jump(float dir)
    {
        rb.linearVelocity = new Vector2(dir * moveTrajectory.x, moveTrajectory.y);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 start = transform.position;
        Vector3 end = start + new Vector3(
            direction * moveTrajectory.x,
            moveTrajectory.y,
            0
        );

        Gizmos.DrawLine(start, end);
        Gizmos.DrawSphere(end, 0.1f);
    }
}
