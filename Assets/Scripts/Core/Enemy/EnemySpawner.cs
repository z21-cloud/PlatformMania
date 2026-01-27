using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using PlatfromMania.Services;

namespace PlatfromMania.Core
{
    public class EnemySpawner : MonoBehaviour, IResettable
    {
        [Header("Spawner Setup")]
        [SerializeField] private LevelContext levelContext;
        [SerializeField] private EnemyPool pool;
        [SerializeField] private List<EnemySpawnPoint> spawnPoints;

        private void Start()
        {
            levelContext.ResetService.Register(this);
            SpawnAll();
        }

        public void SpawnAll()
        {
            foreach (var s in spawnPoints)
            {
                var enemy = pool.Get();
                s.Assign(enemy);
            }
        }

        public void Release(Enemy enemy)
        {
            pool.Release(enemy);
        }

        public void ResetState()
        {
            DespawnAll();
            SpawnAll();
        }

        private void DespawnAll()
        {
            foreach (var s in spawnPoints)
            {
                if (s.CurrentEnemy == null) continue;

                pool.Release(s.CurrentEnemy);
                s.Clear();
            }
        }

        private void OnDisable()
        {
            levelContext.ResetService.Unregister(this);
        }
    }
}

