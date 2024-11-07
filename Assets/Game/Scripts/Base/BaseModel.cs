using UnityEngine;

namespace Game.Scripts.Base
{
    [CreateAssetMenu(fileName = "new Base Model", menuName = "Custom Tools/Base")]
    public class BaseModel : ScriptableObject
    {
        public BaseData Data;
    }
}