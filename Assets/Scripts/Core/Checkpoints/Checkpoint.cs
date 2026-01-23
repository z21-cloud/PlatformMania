using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PlatfromMania.Core
{
    public class Checkpoint : MonoBehaviour, ICheckpoint
    {
        [SerializeField] private Transform respawnPoint;
        public Transform RespawnPoint => respawnPoint;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                CheckpointSystem.Instance.SetCheckpoint(this);
            }
        }
    }
}

