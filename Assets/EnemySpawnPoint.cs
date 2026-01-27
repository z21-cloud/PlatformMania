using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PlatfromMania.Core
{
    public class EnemySpawnPoint : MonoBehaviour
    {
        public Enemy CurrentEnemy { get; private set; }

        public void Assign(Enemy enemy)
        {
            CurrentEnemy = enemy;
            enemy.gameObject.transform.position = transform.position;
            enemy.gameObject.transform.rotation = transform.rotation;
        }

        public void Clear()
        {
            CurrentEnemy = null;
        }
    }
}
