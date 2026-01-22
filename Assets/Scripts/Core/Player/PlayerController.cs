using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlatfromMania.Managers;

namespace PlatfromMania.Core
{
    public class PlayerController : MonoBehaviour
    {
        private HealthComponent health;

        private void Awake()
        {
            health = GetComponent<HealthComponent>();
        }
    }
}

