using Game.Scripts.Spawnables;
using UnityEngine;

namespace Game.Scripts.Enemies
{
    [System.Serializable]
    public struct EnemyData
    {
        [Tooltip("Object pool group reference")]
        public ObjectPoolModel.PoolType Type;
        [Tooltip("Speed of the enemy")]
        public float Speed;
        [Tooltip("Speed with FREEZE of the enemy")]
        public float SpeedFreeze;
        [Tooltip("Number of hitpoints that the enemy supports")]
        public int HitPoints;
        [Tooltip("Number of coins that the enemy sill spawn on dead")]
        public SpawnableData CoinsRewardOnDead;
        [Tooltip("Number of damage that the enemy will do to the base")]
        public int DamageToBase;

        public int GetCoinsReward()
        {
            return Random.Range(CoinsRewardOnDead.MinSpawn, CoinsRewardOnDead.MaxSpawn + 1);
        }
    }
}