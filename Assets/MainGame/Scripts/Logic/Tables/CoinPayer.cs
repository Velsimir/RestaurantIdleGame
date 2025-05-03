using System.Collections;
using System.Collections.Generic;
using MainGame.Scripts.Infrastructure.Factory;
using MainGame.Scripts.Infrastructure.Services;
using MainGame.Scripts.Logic.CoinLogic;
using UnityEngine;

namespace MainGame.Scripts.Logic.Tables
{
    public class CoinPayer : MonoBehaviour
    {
        private const int MaxCoinsInPack = 10;
        
        [SerializeField] private ObjectHoldPoint _coinPlacePoint;
        
        private readonly float _xOffset = 0.6f;
        private readonly float _zOffset = 0.6f;
        
        private int _currentRow = 0;
        private IGameFactory _gameFactory;
        private List<Stack<Coin>> _coins;
        private ObjectStacker<Coin> _objectStacker;
        private WaitForSeconds _waitBetweenSpawnCoins;

        private void Awake()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();
            _coins = new List<Stack<Coin>>();
            _objectStacker = new ObjectStacker<Coin>();
            _waitBetweenSpawnCoins = new WaitForSeconds(0.1f);
        }

        [ContextMenu("SpawnTestCoins")]
        private void SpawnTestCoins()
        {
            StartCoroutine(SpawnPackCoins(1));
        }

        private IEnumerator SpawnPackCoins(int count)
        {
            for (int i = 0; i < count; i++)
            {
                _coins.Add(new Stack<Coin>());
                
                for (int j = 0; j < MaxCoinsInPack; j++)
                {
                    Coin coin = _gameFactory.CreateCoin();
                    coin.transform.position = _objectStacker.GetSpawnPoint(_coins[i], _coinPlacePoint.Transform);
                    _coins[i].Push(coin);
                    
                    yield return _waitBetweenSpawnCoins;
                }

                MoveSpawnPointForNextStack();
            }
        }
        
        private void MoveSpawnPointForNextStack()
        {
            if (_currentRow % 2 == 0)
            {
                _coinPlacePoint.Transform.position += new Vector3(_xOffset, 0, 0);
            }
            else
            {
                _coinPlacePoint.Transform.position += new Vector3(-_xOffset, 0, _zOffset);
            }
    
            _currentRow++;
        }
    }
}