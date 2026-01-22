using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using PlatfromMania.Managers;

namespace PlatfromMania.Core
{
    public class PlayerShooting : MonoBehaviour
    {
        [Header("Shooting setup")]
        [SerializeField] private BulletPool bulletPool;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private float timeBetweenShots = 0.25f;

        private float currentShootTime = 0;
        private bool isShooting = false;
        private void Update()
        {
            HandleInput();

            currentShootTime += Time.deltaTime;
            if (!isShooting || currentShootTime < timeBetweenShots) return;
            
            ReadyToShoot();
            currentShootTime = 0f;
        }

        private void ReadyToShoot()
        {
            Bullet bullet = bulletPool.Get();
            bullet.transform.position = shootPoint.position;

            Vector2 direction = transform.localScale.x > 0 ? transform.right : -transform.right;
            bullet.Init(direction, bulletPool);
        }

        private void HandleInput()
        {
            isShooting = InputManager.Instance.GetMouseButton();
        }
    }
}

