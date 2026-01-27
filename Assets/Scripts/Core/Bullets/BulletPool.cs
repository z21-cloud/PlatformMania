using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlatfromMania.Helpers;

namespace PlatfromMania.Core
{
    public class BulletPool : MonoBehaviour
    {
        [Header("Pool settings")]
        [SerializeField] private Bullet bullet;
        [SerializeField] private Transform bulletTransformParent;
        [SerializeField] private int bulletPoolSize = 10;

        private ObjectPooling<Bullet> pool;

        private void Awake()
        {
            pool = new ObjectPooling<Bullet>(
                bullet, 
                bulletPoolSize, 
                bulletTransformParent);
        }

        public Bullet Get() => pool.Get();
        public void Release(Bullet bullet) => pool.Release(bullet);
    }
}

