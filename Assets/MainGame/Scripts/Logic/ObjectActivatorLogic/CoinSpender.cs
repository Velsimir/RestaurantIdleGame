using System;
using System.Collections;
using MainGame.Scripts.Infrastructure;
using MainGame.Scripts.Infrastructure.Factory;
using MainGame.Scripts.Infrastructure.Services;
using MainGame.Scripts.Logic.PlayerLogic;
using UnityEngine;

namespace MainGame.Scripts.Logic
{
    public class CoinSpender
    {
        private readonly IGameFactory _gameFactory;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly ObjectHoldPoint _coinSpawnPoint;
        private readonly WaitForSeconds _waitBetweenTakeCoins;
        
        private int _coinsToSpawn;
        private Coroutine _spawnCoinsCoroutine;
        private PlayerWallet _playerWallet;

        public CoinSpender(ObjectHoldPoint coinSpawnPoint, int coinsToSpawn, float timeBetweenTakeCoins)
        {
            _coinSpawnPoint = coinSpawnPoint;
            _coinsToSpawn = coinsToSpawn;
            _gameFactory = AllServices.Container.Single<IGameFactory>();
            _coroutineRunner = AllServices.Container.Single<ICoroutineRunner>();
            _waitBetweenTakeCoins = new WaitForSeconds(timeBetweenTakeCoins);

        }

        public event Action AllCoinsPayed;

        public void StopSpawn()
        {
            _coroutineRunner.StopCoroutine(ref _spawnCoinsCoroutine);
            _playerWallet = null;
        }
        
        public void StartSpawn(PlayerWallet playerWallet)
        {
            _playerWallet =  playerWallet;
            _spawnCoinsCoroutine = _coroutineRunner.StartCoroutine(SpawnCoins());
        }

        private IEnumerator SpawnCoins()
        {
            while (_coinsToSpawn > 0)
            {
                if (_playerWallet.TrySpendCoin())
                {
                    _gameFactory.CreateCoin(_coinSpawnPoint.Transform).Interact();
                    _coinsToSpawn--;
                }
                else
                {
                    _coroutineRunner.StopCoroutine(ref _spawnCoinsCoroutine);
                }
                
                yield return _waitBetweenTakeCoins;
            }
            
            AllCoinsPayed?.Invoke();
        }
    }
}