using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace PlatfromMania.Helpers
{
    public class PositionHistory : MonoBehaviour
    {
        [SerializeField] private int bufferSize = 5;
        [SerializeField] private float recordInterval = 0.2f;

        private Queue<Vector3> positions = new Queue<Vector3>();
        private float timer;

        public Vector3 SafePosition { get; private set; }

        private void Update()
        {
            if (!Timer()) return;
            RecordPositon();
        }

        private bool Timer()
        {
            timer += Time.deltaTime;
            if (timer < recordInterval) return false;
            
            timer = 0;
            return true;
        }

        private void RecordPositon()
        {
            if (positions.Count >= bufferSize) positions.Dequeue();

            positions.Enqueue(transform.position);

            SafePosition = positions.Peek();
        }
    }
}

