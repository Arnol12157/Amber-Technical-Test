using UnityEngine;

namespace Game.Scripts.Enemies
{
    [CreateAssetMenu(fileName = "New Enemy Model", menuName = "Custom Tools/Enemy")]
    public class EnemyModel : ScriptableObject
    {
        public EnemyData Data;
    }
}