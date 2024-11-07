using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Turrets
{
    [CreateAssetMenu(fileName = "new Turrets Catalog model", menuName = "Custom Tools/TurretsCatalog")]
    public class TurretsCatalogModel : ScriptableObject
    {
        public List<TurretModel> Turrets;
    }
}