using UnityEngine;

namespace Game.Scripts.Bullets
{
    [CreateAssetMenu(fileName = "New Bullet Model", menuName = "Custom Tools/Bullet")]
    public class BulletModel : ScriptableObject
    {
        public BulletData Data;
    }
}