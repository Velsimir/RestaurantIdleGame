using System.Collections;
using MainGame.Scripts.Infrastructure;
using MainGame.Scripts.Infrastructure.Factory;
using MainGame.Scripts.Infrastructure.Services;
using UnityEngine;

namespace MainGame.Scripts.Logic.Tables
{
    public class CoinPayer
    {
        private readonly IGameFactory _gameFactory;
        private readonly ObjectHoldPoint _coinPlacePoint;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly float _delayBetweenSpawnCoin = 0.1f;
        private readonly WaitForSeconds _waitDelayBetweenSpawnCoin;

        private Coroutine _coinSpawnCoroutine;
        private int _pendingAmount;
        private int _coinsCreatedLastTime;

        public CoinPayer(ObjectHoldPoint coinPlacePoint)
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();
            _coroutineRunner = AllServices.Container.Single<ICoroutineRunner>();
            _coinPlacePoint = coinPlacePoint;
            _waitDelayBetweenSpawnCoin = new WaitForSeconds(_delayBetweenSpawnCoin);
        }

        public void SpawnCoins(int amount)
        {
            _pendingAmount -= _coinsCreatedLastTime;
            _pendingAmount += amount;
            
            _coroutineRunner.StopCoroutine(ref _coinSpawnCoroutine);
            _coinSpawnCoroutine = _coroutineRunner.StartCoroutine(SpawnCoinsWithDelay());
        }

        private IEnumerator SpawnCoinsWithDelay()
        {
            _coinsCreatedLastTime = 0;
            
            for (int i = 0; i < _pendingAmount; i++)
            {
                _gameFactory.CreateCoin(_coinPlacePoint.Transform);
                _coinsCreatedLastTime++;
                
                yield return _waitDelayBetweenSpawnCoin;
            }
        }
    }
}