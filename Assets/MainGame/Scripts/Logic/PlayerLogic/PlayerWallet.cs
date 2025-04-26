using System;
using MainGame.Scripts.Data;
using MainGame.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using YG;

namespace MainGame.Scripts.Logic.PlayerLogic
{
    public class PlayerWallet : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private PlayerCoinCollector _coinCollector;

        public int Coins { get; private set; }
        public event Action Updated;

        private void OnEnable()
        {
            Coins = YG2.saves.PlayerProgress.Coins;
            _coinCollector.CoinCollected += AddCoin;
        }

        private void OnDisable()
        {
            _coinCollector.CoinCollected -= AddCoin;
        }

        private void AddCoin()
        {
            Coins++;
            Updated?.Invoke();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            YG2.saves.PlayerProgress.Coins = Coins;
        }
    }
}