using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlatfromMania.Helpers;

public class SpawnController<T> where T : MonoBehaviour
{
    private readonly IPool<T> pool;
    private readonly List<ISpawnPoint<T>> spawnPoints;
    private readonly System.Action<T> onSpawn;

    public SpawnController(IPool<T> pool,
        List<ISpawnPoint<T>> spawnPoints,
        System.Action<T> onSpawn = null)
    {
        this.pool = pool;
        this.spawnPoints = spawnPoints;
        this.onSpawn = onSpawn;
    }

    public void SpawnAll()
    {
        foreach (var point in spawnPoints)
        {
            var obj = pool.Get();
            onSpawn?.Invoke(obj);
            point.Assign(obj);
        }
    }

    public void DespawnAll()
    {
        foreach (var point in spawnPoints)
        {
            if (point.Current == null) continue;

            pool.Release(point.Current);
            point.Clear();
        }
    }
}
