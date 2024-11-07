using UnityEngine;

namespace Game.Scripts.Turrets
{
    [CreateAssetMenu(fileName = "New Turret Model", menuName = "Custom Tools/Turret")]
    public class TurretModel : ScriptableObject
    {
        public enum BulletType
        {
            Normal
        }
        public TurretData Data;
    }
}