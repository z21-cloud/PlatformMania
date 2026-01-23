using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PlatfromMania.Core
{
    public class WaterComponent : MonoBehaviour, IKillZone
    {
        [Header("Water Setup")]
        [SerializeField] private float damage = 25f;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(damage);
            }

            if(collision.TryGetComponent<PlayerDeathHandler>(out var deathHandler))
            {
                deathHandler.HandleDeath();
            }
        }
    }
}

