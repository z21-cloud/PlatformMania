using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PlatfromMania.Helpers
{
    public interface IPool<T>
    {
        public T Get();
        public void Release(T obj);
    }
}

