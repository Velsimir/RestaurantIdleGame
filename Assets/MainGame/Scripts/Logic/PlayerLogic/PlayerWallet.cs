using System;
using MainGame.Scripts.Data;
using MainGame.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace MainGame.Scripts.Logic.PlayerLogic
{
    public class PlayerWallet : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private PlayerCoinCollector _coinCollector;

        public event Action Updated; 
        
        [field: SerializeField] public int Coins { get; private set; }

        private void OnEnable()
        {
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
            
        }
    }
}