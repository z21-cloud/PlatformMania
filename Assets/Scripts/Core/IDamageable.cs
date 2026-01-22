using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PlatfromMania.Core
{
    public interface IDamageable
    {
        public void TakeDamage(float damage);
        public float CurrentHealth { get; }
        bool IsAlive { get; }
    }
}

