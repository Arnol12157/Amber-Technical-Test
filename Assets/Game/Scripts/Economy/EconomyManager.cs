using System;
using UnityEngine;

namespace Game.Scripts.Economy
{
    public class EconomyManager : MonoBehaviour
    {
        public Action<int> OnUpdateCoins;
        private int _coins = 5;

        public int Coins
        {
            get => _coins;
            private set => _coins = value;
        }

        public void AddCoins(int amount)
        {
            Coins += amount;
            OnUpdateCoins?.Invoke(Coins);
        }
        
        public void SubtractCoins(int amount)
        {
            Coins = Mathf.Max(0, Coins - amount);
            OnUpdateCoins?.Invoke(Coins);
        }

        public bool HasEnoughCoins(int priceAmount)
        {
            return Coins >= priceAmount;
        }
    }
}