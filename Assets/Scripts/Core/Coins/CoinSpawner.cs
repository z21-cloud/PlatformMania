using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlatfromMania.Services;
using System;
using PlatfromMania.Core;

namespace PlatfromMania.Helpers
{
    public class CoinSpawner : MonoBehaviour, IResettable
    {
        [SerializeField] private LevelContext levelContext;
        [SerializeField] private CoinPool pool;
        [SerializeField] private List<CoinSpawnPoint> spawnPoints;

        private SpawnController<Coin> controller;

        private void Awake()
        {
            var points = new List<ISpawnPoint<Coin>>(spawnPoints);
            controller = new SpawnController<Coin>(pool, points);
        }

        private void Start()
        {
            levelContext.ResetService.Register(this);
            controller.SpawnAll();
        }

        public void Release(Coin coin)
        {
            pool.Release(coin);
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


