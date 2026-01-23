using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PlatfromMania.Core
{
    public interface IHealth
    {
        public float CurrentHealth { get; }
        public bool IsAlive { get; }

        public void DebugHealth();
    }
}
