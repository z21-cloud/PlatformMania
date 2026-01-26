using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using PlatfromMania.Services;

namespace PlatfromMania.Helpers
{
    public class PositionHistory : MonoBehaviour
    {
        [Header("Safe Position Setup")]
        [SerializeField] private int bufferSize = 5;
        [SerializeField] private float recordInterval = 0.2f;

        [Header("Ground check")]
        [SerializeField] private Transform groundCheckTransform;
        [SerializeField] private float groundCheckRadius = 0.2f;
        [SerializeField] private LayerMask groundLayer;

        private Queue<Vector3> positions = new Queue<Vector3>();
        private GroundCheckService groundCheckService;
        private float timer;
        private bool isGrounded;

        public Vector3 SafePosition { get; private set; }

        private void Awake()
        {
            groundCheckService = new GroundCheckService();            
        }

        private void Update()
        {
            isGrounded = groundCheckService.Check(
                groundCheckTransform.position, 
                groundCheckRadius, 
                groundLayer);

            if (!Timer()) return;
            if (!isGrounded) return;
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

