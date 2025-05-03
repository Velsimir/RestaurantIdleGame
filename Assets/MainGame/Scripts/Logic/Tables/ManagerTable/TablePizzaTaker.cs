using System;
using System.Collections;
using System.Collections.Generic;
using MainGame.Scripts.Infrastructure;
using MainGame.Scripts.Infrastructure.Services;
using MainGame.Scripts.Logic.PlayerLogic;
using UnityEngine;

namespace MainGame.Scripts.Logic.Tables.ManagerTable
{
    public class TablePizzaTaker : IDisposable
    {
        private readonly ReceptionTable _receptionTable;
        private readonly Stack<Pizza> _pizzas = new Stack<Pizza>();
        private readonly int _maxPizzas;
        private readonly ObjectStacker<Pizza> _objectStacker;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly WaitForSeconds _waitDelayToGetPizza;
        
        private Coroutine _takePizzaCoroutine;
        private bool _isWorking = true;

        public TablePizzaTaker(ReceptionTable receptionTable, int maxPizzas, float delayToGetPizza)
        {
            _receptionTable = receptionTable;
            _maxPizzas = maxPizzas;
            _waitDelayToGetPizza = new WaitForSeconds(delayToGetPizza);
            _objectStacker = new ObjectStacker<Pizza>();
            _coroutineRunner = AllServices.Container.Single<ICoroutineRunner>();

            _receptionTable.TargetEntered += StartTryTakePizzas;
            _receptionTable.TargetExited += StopTakePizzas;
        }

        public bool HasPizzas => _pizzas.Count > 0;
        
        public Pizza GetPizza()
        {
            return _pizzas.Pop();
        }

        public void Dispose()
        {
            _receptionTable.TargetEntered -= StartTryTakePizzas;
            _receptionTable.TargetExited -= StopTakePizzas;
        }

        private void StartTryTakePizzas(Collider collider)
        {
            if (collider.transform.TryGetComponent(out PlayerPizzaTaker interactor))
            {
                _isWorking = true;
                _coroutineRunner.StopCoroutine(ref _takePizzaCoroutine);
                _takePizzaCoroutine = _coroutineRunner.StartCoroutine(TakePizzas(interactor));
            }
        }

        private IEnumerator TakePizzas(PlayerPizzaTaker interactor)
        {
            while (_isWorking)
            {
                if (interactor.HasPizza && _pizzas.Count <= _maxPizzas)
                {
                    TakePizza(interactor);
                }

                yield return _waitDelayToGetPizza;
            }
        }

        private void TakePizza(PlayerPizzaTaker interactor)
        {
            Pizza pizza = interactor.GetPizza();
            pizza.transform.position = _objectStacker.GetSpawnPoint(_pizzas, _receptionTable.HoldPizzaPoint.Transform);
            pizza.SetParent(_receptionTable.Transform);
            _pizzas.Push(pizza);
        }

        private void StopTakePizzas(Collider collider)
        {
            _coroutineRunner.StopCoroutine(ref _takePizzaCoroutine);
            _isWorking = false;
        }
    }
}
