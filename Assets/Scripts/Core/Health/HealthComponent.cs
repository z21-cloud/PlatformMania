using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace PlatfromMania.Core
{
    public class HealthComponent : MonoBehaviour, IDamageable, IHealable, IHealth
    {
        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private float startingHealth = 100f;
        public float CurrentHealth { get; private set; }
        public float MaxHealth => maxHealth;
        public bool IsAlive => CurrentHealth > 0;

        private const float DEATH_THRESHOLD = 0;

        private void Awake()
        {
            CurrentHealth = startingHealth;
        }

        private void Update()
        {
            DebugHealth();
        }

        public void TakeDamage(float damage)
        {
            if (!IsAlive) return;

            CurrentHealth -= damage;
            CurrentHealth = Mathf.Max(CurrentHealth, DEATH_THRESHOLD);

            //event;

            if(CurrentHealth <= DEATH_THRESHOLD)
            {
                Die();
            }
        }

        public void Heal(float amount)
        {
            if (!IsAlive) return;

            CurrentHealth += amount;
            CurrentHealth = Mathf.Max(CurrentHealth, maxHealth);

            //event
        }

        private void Die()
        {
            //event
        }

        public void DebugHealth()
        {
            Debug.Log(CurrentHealth);
        }
    }
}

