using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PlatfromMania.Core
{
    public class HandleGravity : MonoBehaviour
    {
        [Header("Gravity Setup")]
        [SerializeField] private float baseGravity = 2f;
        [SerializeField] private float maxFallSpeed = 18f;
        [SerializeField] private float fallSpeedMultiplier = 2f;

        private Rigidbody2D rb;
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            Gravity();
        }

        private void Gravity()
        {
            if (rb.linearVelocityY < 0)
            {
                rb.gravityScale = baseGravity * fallSpeedMultiplier;
                rb.linearVelocity = new Vector2(rb.linearVelocityX, Mathf.Max(rb.linearVelocityY, -maxFallSpeed));
            }
            else
            {
                rb.gravityScale = baseGravity;
            }
        }
    }
}

