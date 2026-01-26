using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlatfromMania.Core;
using PlatfromMania.Helpers;
using System;

[RequireComponent(typeof(HealthComponent))]
public class PlayerRespawnManager : MonoBehaviour
{
    [Header("Start position set-up")]
    [SerializeField] private Vector3 startPosition;
    private HealthComponent health;
    private PositionHistory positionHistory;
    private void Awake()
    {
        health = GetComponent<HealthComponent>();
        positionHistory = GetComponent<PositionHistory>();

        if (startPosition != Vector3.zero) return;
        startPosition = transform.position;
    }

    public void KillZoneHit()
    {
        if (!health.IsAlive) return;
        RespawnSafePosition();
    }

    private void RespawnSafePosition()
    {
        transform.position = positionHistory.SafePosition;
        Debug.Log("Player Respawn Manager: safe position");
    }

    private void OnEnable()
    {
        health.OnDeath += SoftDeath;
    }

    private void OnDisable()
    {
        health.OnDeath -= SoftDeath;
    }

    private void SoftDeath()
    {
        if(!CheckpointService.Instance.HasCheckpoint())
        {
            transform.position = startPosition;
        }
        else
        {
            transform.position = CheckpointService.Instance.GetCheckpointPosition();
        }
        health.ResetToMaxHealth();
        Debug.Log("Player Respawn Manager: soft death");
    }
}
