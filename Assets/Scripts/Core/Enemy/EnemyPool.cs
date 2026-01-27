using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlatfromMania.Helpers;

namespace PlatfromMania.Core
{
    public class EnemyPool : MonoBehaviour
    {
        [Header("Pool settings")]
        [SerializeField] private Enemy enemy;
        [SerializeField] private Transform enemyTransformParent;
        [SerializeField] private int enemyPoolSize = 10;

        private ObjectPooling<Enemy> pool;
        private void Awake()
        {
            pool = new ObjectPooling<Enemy>(
                enemy,
                enemyPoolSize,
                enemyTransformParent);
        }

        public Enemy Get() => pool.Get();
        public void Release(Enemy enemy) => pool.Release(enemy);
    }
}

