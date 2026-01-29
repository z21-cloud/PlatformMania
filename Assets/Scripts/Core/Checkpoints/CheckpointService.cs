using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using PlatfromMania.UI;
using PlatfromMania.Core;

namespace PlatfromMania.Services
{
    public class CheckpointService : MonoBehaviour, IMessageProvider
    {
        public static CheckpointService Instance;
        private ICheckpoint currentCheckpoint;

        public event Action<string> OnMessageProvider;

        private void Awake()
        {
            if (Instance != this && Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void SetCheckpoint(ICheckpoint checkpoint)
        {
            currentCheckpoint = checkpoint;
            OnMessageProvider?.Invoke("Checkpoint Activated");
        }

        public Vector3 GetCheckpointPosition()
        {
            return currentCheckpoint.CheckpointTransform.position;
        }

        public bool HasCheckpoint()
        {
            return currentCheckpoint.CheckpointTransform.position != Vector3.zero;
        }
    }
}

