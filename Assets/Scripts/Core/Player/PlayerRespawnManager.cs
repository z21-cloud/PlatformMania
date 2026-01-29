using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlatfromMania.Services;
using PlatfromMania.Helpers;
using System;


namespace PlatfromMania.Core
{
    [RequireComponent(typeof(HealthComponent))]
    public class PlayerRespawnManager : MonoBehaviour
    {
        [Header("Level Context")]
        [SerializeField] private LevelContext levelContext;
        [Header("Start position set-up")]
        [SerializeField] private Vector3 startPosition;

        private PlayerController controller;
        private HealthComponent health;
        private void Awake()
        {
            health = GetComponent<HealthComponent>();
            controller = GetComponent<PlayerController>();

            if (startPosition != Vector3.zero) return;
            startPosition = transform.position;
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
            if (!CheckpointService.Instance.HasCheckpoint())
            {
                transform.position = startPosition;
            }
            else
            {
                transform.position = CheckpointService.Instance.GetCheckpointPosition();
            }

            controller.ResetCoins();
            health.ResetToMaxHealth();
            levelContext.ResetService.ResetAll();
            Debug.Log("Player Respawn Manager: soft death");
        }
    }
}

