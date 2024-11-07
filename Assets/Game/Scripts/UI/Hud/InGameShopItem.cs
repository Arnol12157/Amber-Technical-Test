using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Hud
{
    using Turrets;
    using ServiceLocator;
    
    public class InGameShopItem : MonoBehaviour
    {
        [SerializeField] private Text _nameText;
        [SerializeField] private Text _costText;
        private TurretData _structData;
        private TurretsSystem _turretsSystem;

        public TurretData Data
        {
            get => _structData;
            set => _structData = value;
        }

        public void Setup(TurretData data)
        {
            Data = data;
            _nameText.text = Data.Name;
            _costText.text = Data.Price.ToString();
        }

        private void Start()
        {
            _turretsSystem = ServiceLocator.GetService<TurretsSystem>();
        }

        public void SelectTurret()
        {
            _turretsSystem.SelectedTurret = Data;
        }
    }
}