using System.Collections;
using System.Collections.Generic;
using MainGame.Scripts.Infrastructure.Factory;
using MainGame.Scripts.Infrastructure.Services;
using UnityEngine;

namespace MainGame.Scripts.Logic.Tables
{
    public class PizzaBakery : MonoBehaviour
    {
        private const bool IsWorking = true;
        
        [SerializeField] private ObjectHoldPoint _objectHoldPoint;
        [SerializeField] private Transform _transform;
        [SerializeField] private int _maxCountPizza;
        [SerializeField] private float _spawnDelay;

        private WaitForSeconds _wait;
        private Stack<Pizza> _pizzas;
        private IGameFactory _gameFactory;
        private ObjectStacker<Pizza> _objectStacker;
            
        public bool HasPizza => _pizzas.Count > 0;

        private void Awake()
        {
            _pizzas = new Stack<Pizza>();
            _wait = new WaitForSeconds(_spawnDelay);
            _gameFactory = AllServices.Container.Single<IGameFactory>();
            _objectStacker = new ObjectStacker<Pizza>();

            StartSpawn();
        }

        public Pizza GetPizza()
        {
            return _pizzas.Pop();
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
            
            pizza.transform.position = _objectStacker.GetSpawnPoint(_pizzas, _objectHoldPoint.Transform);
            pizza.SetParent(_transform);
            
            _pizzas.Push(pizza);
        }
    }
}