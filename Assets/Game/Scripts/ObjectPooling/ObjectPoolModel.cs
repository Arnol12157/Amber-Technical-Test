using UnityEngine;

namespace Game.Scripts.ObjectPooling
{
    [CreateAssetMenu(fileName = "Object Pool Model", menuName = "Custom Tools/Pool Model")]
    public class ObjectPoolModel : ScriptableObject
    {
        public enum PoolType
        {
            SmallEnemy,
            BigEnemy,
            TurretFreezing,
            TurretRegular,
            NormalBullet,
            HealthBar
        }

        [Tooltip("Type of pool")]
        public PoolType Type;
        [Tooltip("Reference to the prefab")]
        public GameObject Prefab;
        [Tooltip("Size of the pool")]
        public int PoolSize = 10;
    }
}