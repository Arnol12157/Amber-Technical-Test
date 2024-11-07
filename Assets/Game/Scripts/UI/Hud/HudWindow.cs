using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Hud
{
    public class HudWindow : Window
    {
        [SerializeField] private Text _coinsText;
        [SerializeField] private InGameShopPanel _inGameShopPanel;
        private StringBuilder _coinsTextBuilder = new StringBuilder();

        public override void Start()
        {
            base.Start();
            _inGameShopPanel.Setup();
        }

        public void UpdateCoinsView(int coins)
        {
            _coinsTextBuilder.Append("Coins: ");
            _coinsTextBuilder.Append(coins.ToString());
            _coinsText.text = _coinsTextBuilder.ToString();
            _coinsTextBuilder.Clear();
        }
    }
}