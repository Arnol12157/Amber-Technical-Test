using UnityEngine;

namespace Game.Scripts.Spawnables
{
    [System.Serializable]
    public struct SpawnableData
    {
        public enum SpawnableType
        {
            Coin
        }

        [Tooltip("Type of item to spawn")]
        public SpawnableType Type;
        [Tooltip("Min quantity to spawn")]
        public int MinSpawn;
        [Tooltip("Max quantity to spawn")]
        public int MaxSpawn;
    }
}