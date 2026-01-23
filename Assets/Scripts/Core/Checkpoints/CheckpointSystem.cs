using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PlatfromMania.Core
{
    public class CheckpointSystem : MonoBehaviour
    {
        public static CheckpointSystem Instance { get; private set; }

        private ICheckpoint currentCheckpoint;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void SetCheckpoint(ICheckpoint checkpoint)
        {
            Debug.Log($"Checkpoint System: Checkpoint reached!");
            currentCheckpoint = checkpoint;
        }

        public Vector3 GetRespawnPosition()
        {
            return currentCheckpoint.RespawnPoint.position;
        }
    }
}

