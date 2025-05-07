using System;
using System.Collections;
using MainGame.Scripts.Infrastructure;
using MainGame.Scripts.Infrastructure.Factory;
using MainGame.Scripts.Infrastructure.Services;
using MainGame.Scripts.Logic.PlayerLogic;
using TMPro;
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
        private readonly TMP_Text _textCoinsLeft;
        private Coroutine _spawnCoinsCoroutine;
        private PlayerWallet _playerWallet;

        public CoinSpender(ObjectHoldPoint coinSpawnPoint, int coinsToSpawn, float timeBetweenTakeCoins,
            TMP_Text textCoinsLeft)
        {
            _coinSpawnPoint = coinSpawnPoint;
            _coinsToSpawn = coinsToSpawn;
            _textCoinsLeft = textCoinsLeft;
            _gameFactory = AllServices.Container.Single<IGameFactory>();
            _coroutineRunner = AllServices.Container.Single<ICoroutineRunner>();
            _waitBetweenTakeCoins = new WaitForSeconds(timeBetweenTakeCoins);
            _textCoinsLeft.text = _coinsToSpawn.ToString();
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
                    _textCoinsLeft.text = _coinsToSpawn.ToString();
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