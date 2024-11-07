using System.Collections.Generic;
using Game.Scripts.Spawnables;
using UnityEngine;

namespace Game.Scripts.Turrets
{
    [System.Serializable]
    public struct TurretData
    {
        [Tooltip("Type of pool")]
        public ObjectPoolModel.PoolType Type;
        [Tooltip("Name of the turret")]
        public string Name;
        [Tooltip("Price of the turret")]
        public int Price;
        [Tooltip("Delay time between each bullet fired by the turret")]
        public float BulletSpawnDelay;
        [Tooltip("Type of bullet")]
        public TurretModel.BulletType BulletType;
    }
}