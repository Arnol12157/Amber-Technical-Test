using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Scripts.ObjectPooling
{
    [Serializable]
    public class ObjectPool
    {
        public GameObject[] Pool = new GameObject[]{};
        public int NextObject = 0;
    }

    public class ObjectPooling : MonoBehaviour
    {
        [SerializeField] private List<ObjectPoolModel> _poolConfigs;

        public Dictionary<ObjectPoolModel.PoolType, ObjectPool> Pools
        {
            get => _pools;
            set => _pools = value;
        }

        private Dictionary<ObjectPoolModel.PoolType, ObjectPool> _pools = new();

        private void Awake()
        {
            foreach (var pool in _poolConfigs)
            {
                ObjectPool objectPool = new ObjectPool();
                objectPool.Pool = new GameObject[pool.PoolSize];
                GameObject parent = new GameObject();
                parent.transform.SetParent(transform);
                parent.name = pool.Type.ToString();
                for (int i = 0; i < pool.PoolSize; i++)
                {
                    objectPool.Pool[i] = Instantiate(pool.Prefab, parent.transform);
                    objectPool.Pool[i].SetActive(false);
                }
                Pools.Add(pool.Type, objectPool);
            }
        }

        public GameObject GetObject(ObjectPoolModel.PoolType poolType)
        {
            GameObject obj = Pools[poolType].Pool[Pools[poolType].NextObject];
            Pools[poolType].NextObject = (Pools[poolType].NextObject + 1) % Pools[poolType].Pool.Length;
            obj.SetActive(true);

            return obj;
        }

        public void ReleaseObject(GameObject obj)
        {
            obj.SetActive(false);
        }

        private List<GameObject> GetPool(ObjectPoolModel.PoolType poolType, bool onlyActives)
        {
            if (!Pools.ContainsKey(poolType))
                return new List<GameObject>();
            
            List<GameObject> pool = Pools[poolType].Pool.ToList();
            if (onlyActives)
            {
                pool = new List<GameObject>();
                foreach (var objectPool in Pools[poolType].Pool)
                {
                    if(objectPool.activeSelf)
                        pool.Add(objectPool);
                }
            }
            return pool;
        }
        
        public GameObject GetClosestEnemy()
        {
            List<GameObject> enemiesPool = GetPool(ObjectPoolModel.PoolType.SmallEnemy, true);
            enemiesPool.AddRange(GetPool(ObjectPoolModel.PoolType.BigEnemy, true));
                
            GameObject closestEnemy = null;
            float shortestDistance = Mathf.Infinity;
            Vector3 currentPosition = transform.position;

            foreach (GameObject enemy in enemiesPool)
            {
                float distance = Vector3.Distance(enemy.transform.position, currentPosition);

                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    closestEnemy = enemy;
                }
            }

            return closestEnemy;
        }

        public bool AreActiveEnemies()
        {
            List<GameObject> enemiesPool = GetPool(ObjectPoolModel.PoolType.SmallEnemy, true);
            enemiesPool.AddRange(GetPool(ObjectPoolModel.PoolType.BigEnemy, true));
            return enemiesPool.Count > 0;
        }
    }
}