using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace PlatfromMania.Core
{
    public class Bullet : MonoBehaviour
    {
        [Header("Bullet Setup")]
        [SerializeField] private GameObject bulletSprite;
        [SerializeField] private float movementSpeed = 5f;
        [SerializeField] private float rotationAngle = 1f;
        [SerializeField] private float lifeTime = 5f;
        [SerializeField] private float damage = 25f;

        private const float Z_ANGLE_CONST = 1f;

        private float currentLifeTime;
        private Vector2 direction;
        private BulletPool pool;

        public void Init(Vector2 direction, BulletPool pool)
        {
            this.direction = direction.normalized;
            this.pool = pool;
            currentLifeTime = 0;
        }

        private void Update()
        {
            HandleMovement();
            LifeTimer();
        }

        private void HandleMovement()
        {
            BulletMovement();
            SpriteRotation();
        }

        private void BulletMovement()
        {
            transform.Translate(direction * movementSpeed * Time.deltaTime);
        }

        private void SpriteRotation()
        {
            Quaternion rotationZ = Quaternion.AngleAxis(rotationAngle, new Vector3(0, 0, Z_ANGLE_CONST));
            bulletSprite.transform.rotation *= rotationZ;
        }

        private void LifeTimer()
        {
            currentLifeTime += Time.deltaTime;
            if (currentLifeTime >= lifeTime)
            {
                ReturnToPool();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(damage);
                ReturnToPool();
            }
        }

        private void ReturnToPool()
        {
            pool.Release(this);
        }
    }
}

