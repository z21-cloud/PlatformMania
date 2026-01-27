using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PlatfromMania.Services
{
    public class LevelContext : MonoBehaviour
    {
        public ResetService ResetService { get; private set; }

        private void OnEnable()
        {
            ResetService = new ResetService();
        }
    }
}

