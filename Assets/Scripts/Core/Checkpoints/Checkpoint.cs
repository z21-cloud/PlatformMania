using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Checkpoint : MonoBehaviour, ICheckpoint
{
    [SerializeField] private Transform checkpointTransform;
    public Transform CheckpointTransform => checkpointTransform;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //translate checkpoint transform;
            Debug.Log("Checkpoint activated");
            CheckpointService.Instance.SetCheckpoint(this);
        }
    }
}
