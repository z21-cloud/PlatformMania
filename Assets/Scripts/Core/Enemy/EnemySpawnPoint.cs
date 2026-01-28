using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlatfromMania.Core;


public class EnemySpawnPoint : MonoBehaviour, ISpawnPoint<Enemy>
{
    public Enemy Current { get; private set; }

    public void Assign(Enemy enemy)
    {
        Current= enemy;
        enemy.transform.SetPositionAndRotation(
            transform.position,
            transform.rotation
        );
    }

    public void Clear()
    {
        Current = null;
    }
}
