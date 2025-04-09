using System.Collections;
using System.Collections.Generic;
using MainGame.Scripts.Infrastructure.Factory;
using MainGame.Scripts.Infrastructure.Services;
using UnityEngine;

namespace MainGame.Scripts.Logic
{
    public class PizzaBakery : MonoBehaviour
    {
        private const bool IsWorking = true;
        
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _transform;
        [SerializeField] private int _maxCountPizza;
        [SerializeField] private float _spawnDelay;

        private WaitForSeconds _wait;
        private Stack<Pizza> _pizzas;
        private IGameFactory _gameFactory;
            
        public bool HasPizza => _pizzas.Count > 0;

        private void Awake()
        {
            _pizzas = new Stack<Pizza>();
            _wait = new WaitForSeconds(_spawnDelay);
            _gameFactory = AllServices.Container.Single<IGameFactory>();

            StartSpawn();
        }
    
        private void StartSpawn()
        {
            StartCoroutine(SpawnProcess());
        }

        private IEnumerator SpawnProcess()
        {
            while (IsWorking)
            {
                yield return _wait;

                if (_pizzas.Count <= _maxCountPizza)
                {
                    SpawnPizza();
                }
            }
        }

        private void SpawnPizza()
        {
            Pizza pizza = _gameFactory.CreatePizza();
            
            pizza.transform.position = GetSpawnPoint();
            pizza.SetParent(_transform);
            
            _pizzas.Push(pizza);
        }

        private Vector3 GetSpawnPoint()
        {
            if (_pizzas.Count > 0)
            {
                float yPosition = CalculateYPosition();
                return new Vector3(_spawnPoint.transform.position.x, yPosition, _spawnPoint.transform.position.z);
            }
            else
            {
                return _spawnPoint.position;
            }
        }

        private float CalculateYPosition()
        {
            return _pizzas.Peek().Bounds.max.y;
        }

        public Pizza GetPizza()
        {
            return _pizzas.Pop();
        }
    }
}