using Game.Scripts.Enemies;
using UnityEngine;

namespace Game.Scripts.WaveSystem
{
    [System.Serializable]
    public struct WaveData
    {
        [Tooltip("Reference to the enemy model")]
        public EnemyModel WaveEnemy;
        [Tooltip("Number of enemies to spawn on the wave")]
        public int WaveQuantity;
        [Tooltip("Daley time of the wave")]
        public float WaveDelay;
    }
}