using System.Collections.Generic;
using Game.Scripts.Turrets;
using UnityEngine;

namespace Game.Scripts.UI.Hud
{
    public class InGameShopPanel : MonoBehaviour
    {
        [SerializeField] private Transform _itemsParent;
        [SerializeField] private GameObject _inGameShopItem;
        [SerializeField] private TurretsCatalogModel _turretsList;

        public void Setup()
        {
            CreateInGameShopItems();
        }

        private void CreateInGameShopItems()
        {
            foreach (var turret in _turretsList.Turrets)
            {
                GameObject inGameShopItem = Instantiate(_inGameShopItem, _itemsParent);
                inGameShopItem.GetComponent<InGameShopItem>().Setup(turret.Data);
            }
        }
    }
}