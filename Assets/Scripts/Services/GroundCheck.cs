using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PlatfromMania.Services
{
    public class GroundCheck : MonoBehaviour
    {
        [Header("Проверка земли")]
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundCheckRadius = 0.2f;
        [SerializeField] private LayerMask groundLayer;

        public bool IsGrounded => isGrounded;

        private bool isGrounded;

        void Update()
        {
            CheckGround();
        }

        private void CheckGround()
        {
            isGrounded = Physics2D.OverlapCircle(
                groundCheck.position, 
                groundCheckRadius, 
                groundLayer
                );
        }

        private void OnDrawGizmos()
        {
            if (groundCheck != null)
            {
                // Красный круг - проверка земли снизу
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
            }
        }
    }
}

