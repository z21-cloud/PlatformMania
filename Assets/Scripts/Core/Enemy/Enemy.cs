using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlatfromMania.Services;
using System;


namespace PlatfromMania.Core
{
    public class Enemy : MonoBehaviour
    {
        [Header("Enemy setup")]
        [SerializeField] private HealthComponent health;
        [SerializeField] private float damage = 25f;

        private void Awake()
        {
            health = GetComponent<HealthComponent>();
            health.OnDeath += HandleDeath;
        }

        private void HandleDeath()
        {
            gameObject.SetActive(false);
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

