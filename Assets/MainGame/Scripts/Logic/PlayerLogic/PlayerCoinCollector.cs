using System;
using MainGame.Scripts.Logic.CoinLogic;
using UnityEngine;

namespace MainGame.Scripts.Logic.PlayerLogic
{
    public class PlayerCoinCollector : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        
        public event Action CoinCollected;

        private void OnEnable()
        {
            _triggerObserver.CollusionEntered += TryGetCoin;
        }
        
        private void OnDisable()
        {
            _triggerObserver.CollusionEntered -= TryGetCoin;
        }

        private void TryGetCoin(Collider obj)
        {
            if (obj.TryGetComponent(out Coin coin))
            {
                coin.Disappear();
                CoinCollected?.Invoke();
            }
        }
    }
}