using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class GameObjectsPool
    {
        public GameObjectsPool(MonoBehaviour obj, int size)
        {
            pooledObj = obj;
            pooledObj.gameObject.SetActive(false);
            pool = new Queue<MonoBehaviour>();
            pool.Enqueue(pooledObj);
            for (var i = 1; i < size; i++)
            {
                pool.Enqueue(Object.Instantiate(pooledObj, pooledObj.transform.parent));
            }
        }
        
        private readonly Queue<MonoBehaviour> pool;
        private readonly MonoBehaviour pooledObj;

        public T Get<T>() where T : MonoBehaviour
        {
            var obj = pool.Count == 0 ? Object.Instantiate(pooledObj, pooledObj.transform.parent) : pool.Dequeue();
            obj.gameObject.SetActive(true);
            return (T)obj;
        }

        public void Return(MonoBehaviour obj)
        {
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }
}