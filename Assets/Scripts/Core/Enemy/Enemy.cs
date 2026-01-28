using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlatfromMania.Services;
using System;


namespace PlatfromMania.Core
{
    public class Enemy : MonoBehaviour, IResettable
    {
        [Header("Enemy setup")]
        [SerializeField] private HealthComponent health;
        [SerializeField] private float damage = 25f;

        private EnemySpawner enemySpawn;

        public void Initialize(EnemySpawner spawner)
        {
            enemySpawn = spawner;
        }

        private void Awake()
        {
            health = GetComponent<HealthComponent>();
            health.OnDeath += HandleDeath;
        }

        private void HandleDeath()
        {
            enemySpawn?.Release(this);
        }

        public void ResetState()
        {
            health.ResetToMaxHealth();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(damage);
            }
        }
    }
}

