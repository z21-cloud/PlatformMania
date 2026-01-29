using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PlatfromMania.Core
{
    public class KillZoneComponent : MonoBehaviour, IKillZone
    {
        [Header("Kill Zone Setup")]
        [SerializeField] private float damage = 100f;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(damage);
            }
        }
    }
}

