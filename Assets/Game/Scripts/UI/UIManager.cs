using Game.Scripts.DependencyInjection;
using Game.Scripts.Economy;
using Game.Scripts.GameLogic;
using Game.Scripts.UI.Hud;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class UIManager : MonoBehaviour, IGameStateObserver
    {
        [Inject] private WindowGenerator _windowGenerator;
        [Inject] private EconomyManager _economyManager;
        [Inject] private GameManager _gameManager;

        private void Start()
        {
            _economyManager.OnUpdateCoins += OnUpdateCoins;
            _gameManager.SubscribeToGameStateChanges(this);
        }

        private void OnUpdateCoins(int coins)
        {
            GetHudWindow().UpdateCoinsView(coins);
        }

        public void OnGameStateUpdated(GameManager.GameState state)
        {
            switch (state)
            {
                case GameManager.GameState.Lose:
                    _windowGenerator.ShowLoserWindow();
                    break;
                case GameManager.GameState.Win:
                    _windowGenerator.ShowWinnerWindow();
                    break;
            }
        }

        public HudWindow GetHudWindow()
        {
            return _windowGenerator.GetHudWindow();
        }
    }
}