using System.Collections.Generic;
using Game.Scripts.Enemies;
using UnityEngine;

namespace Game.Scripts.WaveSystem
{
    [CreateAssetMenu(fileName = "New Wave Model", menuName = "Custom Tools/Waves")]
    public class WaveModel : ScriptableObject, Wave
    {
        public List<WaveData> WaveData;
        
        public void Generate()
        {
            
        }
    }
}