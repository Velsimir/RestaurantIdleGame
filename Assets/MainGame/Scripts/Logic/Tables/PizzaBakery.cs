using System.Collections;
using System.Collections.Generic;
using MainGame.Scripts.Infrastructure.Factory;
using MainGame.Scripts.Infrastructure.Services;
using MainGame.Scripts.Logic.PlayerLogic;
using UnityEngine;

namespace MainGame.Scripts.Logic.Tables
{
    public class PizzaBakery : MonoBehaviour, IActivatable, IGiveble
    {
        private const bool IsWorking = true;
        
        [SerializeField] private ObjectHoldPoint _objectHoldPoint;
        [SerializeField] private Transform _transform;
        [SerializeField] private int _maxCountPizza;
        [SerializeField] private float _spawnDelay;

        private WaitForSeconds _wait;
        private Stack<ITakable> _pizzas;
        private IGameFactory _gameFactory;
        private ObjectStacker _objectStacker;

        public bool HasObjects => _pizzas.Count > 0;

        private void Awake()
        {
            _pizzas = new Stack<ITakable>();
            _wait = new WaitForSeconds(_spawnDelay);
            _gameFactory = AllServices.Container.Single<IGameFactory>();
            _objectStacker = new ObjectStacker();

            StartSpawn();
        }

        public ITakable GetObject()
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
            ITakable pizza = _gameFactory.CreatePizza();
            
            pizza.Take(_transform,_objectStacker.GetSpawnPoint(_pizzas, _objectHoldPoint.Transform));
            
            _pizzas.Push(pizza);
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }
    }
}