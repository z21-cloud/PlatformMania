using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlatfromMania.Helpers;
using System;

namespace PlatfromMania.Core
{
    public class PlayerDeathHandler : MonoBehaviour
    {
        [SerializeField] private PositionHistory positionHistory;
        [SerializeField] private Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void HandleDeath()
        {
            Respawn();
        }

        private void Respawn()
        {
            rb.linearVelocity = Vector2.zero;
            transform.position = CheckpointSystem.Instance.GetRespawnPosition();
        }
    }
}

