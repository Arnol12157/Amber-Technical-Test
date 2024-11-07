using UnityEngine;

namespace Game.Scripts.Bullets
{
    [System.Serializable]
    public struct BulletData
    {
        [Tooltip("Speed of the bullet")]
        public float Speed;
        [Tooltip("Damage of the bullet")]
        public int DamageAmount;
    }
}