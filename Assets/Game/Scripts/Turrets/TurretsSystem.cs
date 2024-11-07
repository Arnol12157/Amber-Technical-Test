using Game.Scripts.DependencyInjection;
using Game.Scripts.Economy;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Scripts.Turrets
{
    public class TurretsSystem : MonoBehaviour
    {
        private TurretData _selectedTurret = new TurretData();
        [Inject] private EconomyManager _economyManager;
        [Inject] private SpawnSystem.SpawnSystem _spawnSystem;

        public TurretData SelectedTurret
        {
            get => _selectedTurret;
            set => _selectedTurret = value;
        }

        private void Update()
        {
            if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0) && CanSpawnTurret())
            {
                _spawnSystem.SpawnOnClick(SelectedTurret.Type);
                _economyManager.SubtractCoins(SelectedTurret.Price);
            }
        }
        
        private bool CanSpawnTurret()
        {
            return !SelectedTurret.Equals(default(TurretData)) &&
                   _economyManager.HasEnoughCoins(SelectedTurret.Price);
        }
    }
}