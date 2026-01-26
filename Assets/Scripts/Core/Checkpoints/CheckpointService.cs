using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckpointService : MonoBehaviour
{
    public static CheckpointService Instance;
    private ICheckpoint currentCheckpoint;
    
    private void Awake()
    {
        if(Instance != this && Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void SetCheckpoint(ICheckpoint checkpoint)
    {
        currentCheckpoint = checkpoint;
    }

    public Vector3 GetCheckpointPosition()
    {
        return currentCheckpoint.CheckpointTransform.position;
    }

    public bool HasCheckpoint() => currentCheckpoint.CheckpointTransform.position != Vector3.zero;
}
