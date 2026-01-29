using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PlatfromMania.Core
{
    public interface ISpawnPoint<T> where T : MonoBehaviour
    {
        public T Current { get; }
        public void Assign(T obj);
        public void Clear();
    }
}

