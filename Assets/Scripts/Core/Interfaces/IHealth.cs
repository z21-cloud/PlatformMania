using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace PlatfromMania.Core
{
    public interface IHealth
    {
        public float CurrentHealth { get; }
        public float MaxHealth { get; }
        public bool IsAlive { get; }

        //public void DebugHealth();
        public event Action<float> OnHealthChanged;
    }
}
