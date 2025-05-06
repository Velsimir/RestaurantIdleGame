using System;
using System.Collections.Generic;
using MainGame.Scripts.Data;
using MainGame.Scripts.Infrastructure.Services.ObjectSpawner;
using MainGame.Scripts.Infrastructure.Services.PersistentProgress;
using MainGame.Scripts.Logic.CoinLogic;
using UnityEngine;
using YG;

namespace MainGame.Scripts.Logic.PlayerLogic
{
    public class PlayerWallet : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private TriggerObserver _coinObserver;
        
        public int Coins { get; private set; }
        public event Action Updated;

        private void OnEnable()
        {
            _coinObserver.CollusionEntered += TryAddCoins;
        }

        private void OnDisable()
        {
            _coinObserver.CollusionEntered -= TryAddCoins;
        }

        public bool TrySpendCoin()
        {
            if (Coins > 0)
            {
                Coins--;
                Updated?.Invoke();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void LoadProgress(PlayerProgress progress)
        {
            Coins = progress.Coins;
            Updated?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            YG2.saves.PlayerProgress.Coins = Coins;
        }

        private void TryAddCoins(Collider obj)
        {
            if (obj.TryGetComponent(out Coin coin))
            {
                coin.Interact();
                coin.Disappeared += AddCoin;
            }
        }

        private void AddCoin(ISpawnable coin)
        {
            coin.Disappeared -= AddCoin;
            Coins++;
            Updated?.Invoke();
        }
    }
}