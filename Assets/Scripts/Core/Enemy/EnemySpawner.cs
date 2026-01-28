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

        private SpawnController<Enemy> controller;

        private void Awake()
        {
            var points = new List<ISpawnPoint<Enemy>>(spawnPoints);
            controller = new SpawnController<Enemy>(
                pool, 
                points,
                onSpawn: Callback
                );
        }

        private void Callback(Enemy enemy)
        {
            enemy.Initialize(this);
        }

        private void Start()
        {
            levelContext.ResetService.Register(this);
            controller.SpawnAll();
        }

        public void Release(Enemy enemy)
        {
            pool.Release(enemy);
        }

        public void ResetState()
        {
            controller.DespawnAll();
            controller.SpawnAll();
        }

        private void OnDisable()
        {
            levelContext.ResetService.Unregister(this);
        }
    }
}

