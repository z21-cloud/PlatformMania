using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PlatfromMania.Helpers
{
    public class ObjectPooling<T> where T : MonoBehaviour
    {
        private T prefab;
        private Stack<T> available;
        private List<T> allObjects;
        private Transform parent;
        
        public ObjectPooling(T prefab, int initialSize = 10, Transform parent = null)
        {
            this.prefab = prefab;
            this.parent = parent;
            
            allObjects = new List<T>(initialSize);
            available = new Stack<T>(initialSize);

            for (int i = 0; i < initialSize; i++)
            {
                CreateNewObject();
            }
        }

        private T CreateNewObject()
        {
            var obj = GameObject.Instantiate(prefab, parent);
            obj.gameObject.SetActive(false);
            
            available.Push(obj);
            allObjects.Add(obj);
            
            return obj;
        }

        public T Get()
        {
            if(available.Count > 0)
            {
                var obj = available.Pop();
                obj.gameObject.SetActive(true);
                return obj;
            }

            var newObj = CreateNewObject();
            available.Pop();
            newObj.gameObject.SetActive(true);
            return newObj;
        }

        public void Release(T obj)
        {
            if (obj == null) return;
            obj.gameObject.SetActive(false);
            if (!available.Contains(obj))
            {
                available.Push(obj);
            }
        }

        public void Clear()
        {
            foreach (var obj in allObjects)
            {
                if(obj != null)
                {
                    GameObject.Destroy(obj.gameObject);
                }

                available.Clear();
                allObjects.Clear();
            }
        }
    }
}
