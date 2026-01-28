using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlatfromMania.Helpers;

namespace PlatfromMania.Core
{
    public class EnemyPool : MonoBehaviour, IPool<Enemy>
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

        public Enemy Get()
        {
            var enemy = pool.Get();
            if(enemy is IResettable resettable)
            {
                resettable.ResetState();
            }

            return enemy;
        }
        public void Release(Enemy enemy)
        {
            Debug.Log($"Pool.Release: Enemy health BEFORE = {enemy.GetComponent<HealthComponent>().CurrentHealth}");
            pool.Release(enemy);
            Debug.Log($"Pool.Release: Enemy health AFTER = {enemy.GetComponent<HealthComponent>().CurrentHealth}");
        }
    }
}



