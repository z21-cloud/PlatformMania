using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlatfromMania.Services;

namespace PlatfromMania.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private float followSpeed = 2.5f;
        [SerializeField] private Vector2 offset = new Vector2(0, 2);

        private Transform playerPosition;
        private Vector3 velocity = Vector3.zero;

        // Update is called once per frame
        private void Update()
        {
            if (playerPosition == null) playerPosition = PlayerLocator.Player.transform;
            Vector3 newCameraPos = new Vector3(playerPosition.position.x + offset.x, playerPosition.position.y + offset.y, -10);
            transform.position = Vector3.Slerp(transform.position, newCameraPos, followSpeed * Time.deltaTime);
        }
    }
}

