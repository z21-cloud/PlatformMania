using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PlatfromMania.Core
{
    public interface IHealable
    {
        public void Heal(float value);
        public float CurrentHealth { get; }
        bool IsAlive { get; }
    }
}

