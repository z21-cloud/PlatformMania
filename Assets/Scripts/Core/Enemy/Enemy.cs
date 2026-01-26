using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlatfromMania.Services;
using System;


namespace PlatfromMania.Core
{
    public class Enemy : MonoBehaviour, IDirectionProvider
    {
        private enum EnemyState
        {
            Patrol,
            Chase
        }
        [SerializeField] protected Vector2 moveTrajectory = new Vector2(2f, 2f);
        [SerializeField] protected float timeBetweenSteps = 0.25f;

        [Header("Ground Check Setup")]
        [SerializeField] private Transform groundCheckTransform;
        [SerializeField] private float groundCheckDistance = 0.5f;
        [SerializeField] private LayerMask groundMask;

        private GroundCheckService groundCheck;
        private EnemyPool enemyPool;
        private Rigidbody2D rb;

        private float direction = 1f;
        private float timer = 0;
        private bool isGrounded;
        private EnemyState state = EnemyState.Patrol;
        public float Direction => direction;

        private void Awake()
        {
            groundCheck = new GroundCheckService();
            rb = GetComponent<Rigidbody2D>();
        }


        private void Update()
        {
            isGrounded = groundCheck.CheckRaycast(
                groundCheckTransform.position, 
                Vector2.down, 
                groundCheckDistance, 
                groundMask);

            timer += Time.deltaTime;
            if(timer >= timeBetweenSteps)
            {
                Act();
                timer = 0;
            }
        }

        private void Act()
        {
            switch(state)
            {
                case EnemyState.Patrol:
                    Patrol();
                    break;
                case EnemyState.Chase:
                    Chase();
                    break;
            }
        }

        private void Chase()
        {
            throw new NotImplementedException();
        }

        private void Patrol()
        {
            if(!isGrounded)
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
}

