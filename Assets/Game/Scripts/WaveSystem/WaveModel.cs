using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.WaveSystem
{
    [CreateAssetMenu(fileName = "New Wave Model", menuName = "Custom Tools/Waves")]
    public class WaveModel : ScriptableObject, IWave
    {
        public List<WaveData> WaveData;
        
        public void Generate()
        {
            
        }
    }
}