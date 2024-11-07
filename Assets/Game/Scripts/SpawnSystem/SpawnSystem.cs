using System;
using System.Collections.Generic;
using Game.Scripts.DependencyInjection;
using Game.Scripts.Economy;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace Game.Scripts.SpawnSystem
{
    using ObjectPooling;
    
    public class SpawnSystem : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _spawnPoints;
        [SerializeField] private LayerMask _terrainLayerMask;
        [Inject] private ObjectPooling _objectPooling;

        private GameObject GetSpawnPoint()
        {
            int index = Random.Range(0, _spawnPoints.Count);
            return _spawnPoints[index];
        }

        public GameObject Spawn(ObjectPoolModel.PoolType poolType)
        {
            GameObject obj = _objectPooling.GetObject(poolType);
            obj.transform.position = GetSpawnPoint().transform.position;
            return obj;
        }
        
        public void SpawnOnClick(ObjectPoolModel.PoolType poolType)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _terrainLayerMask))
            {
                GameObject obj = _objectPooling.GetObject(poolType);

                Vector3 position = hit.point;
                position.y = Terrain.activeTerrain.SampleHeight(position);

                obj.transform.position = position;
            }
        }
        
        public GameObject SpawnOnPosition(ObjectPoolModel.PoolType poolType, Vector3 position)
        {
            GameObject obj = _objectPooling.GetObject(poolType);
            obj.transform.position = position;
            return obj;
        }
    }
}