using UnityEngine;

namespace Game.Scripts.Interfaces
{
    public interface IDamageable
    {
        void DoDamage(int damageAmount);
        int GetHealth();
        GameObject GetGameObject();
    }
}