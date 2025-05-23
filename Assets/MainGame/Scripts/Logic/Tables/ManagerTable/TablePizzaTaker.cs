using System;
using System.Collections;
using System.Collections.Generic;
using MainGame.Scripts.Infrastructure;
using MainGame.Scripts.Infrastructure.Services;
using MainGame.Scripts.Logic.PlayerLogic;
using UnityEngine;

namespace MainGame.Scripts.Logic.Tables.ManagerTable
{
    public class TablePizzaTaker : IDisposable, IGiveble
    {
        private readonly ReceptionTable _receptionTable;
        private readonly Stack<ITakable> _pizzas = new Stack<ITakable>();
        private readonly int _maxPizzas;
        private readonly ObjectStacker _objectStacker;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly WaitForSeconds _waitDelayToGetPizza;
        
        private Coroutine _takePizzaCoroutine;
        private bool _isWorking = true;

        public TablePizzaTaker(ReceptionTable receptionTable, int maxPizzas, float delayToGetPizza)
        {
            _receptionTable = receptionTable;
            _maxPizzas = maxPizzas;
            _waitDelayToGetPizza = new WaitForSeconds(delayToGetPizza);
            _objectStacker = new ObjectStacker();
            _coroutineRunner = AllServices.Container.Single<ICoroutineRunner>();

            _receptionTable.TargetEntered += StartTryTakePizzas;
            _receptionTable.TargetExited += StopTakePizzas;
        }

        public bool HasObjects => _pizzas.Count > 0;

        public ITakable GetObject()
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
            if (collider.transform.TryGetComponent(out ObjectTaker objectTaker))
            {
                _isWorking = true;
                _coroutineRunner.StopCoroutine(ref _takePizzaCoroutine);
                _takePizzaCoroutine = _coroutineRunner.StartCoroutine(TakePizzas(objectTaker));
            }
        }

        private IEnumerator TakePizzas(ObjectTaker objectTaker)
        {
            while (_isWorking)
            {
                if (objectTaker.HasObjects && _pizzas.Count <= _maxPizzas)
                {
                    TakePizza(objectTaker);
                }

                yield return _waitDelayToGetPizza;
            }
        }

        private void TakePizza(ObjectTaker objectTaker)
        {
            ITakable pizza = objectTaker.GetObject();
            pizza.Take(_receptionTable.HoldPizzaPoint.Transform, _objectStacker.GetSpawnPoint(_pizzas, _receptionTable.HoldPizzaPoint.Transform));
            _pizzas.Push(pizza);
        }

        private void StopTakePizzas(Collider collider)
        {
            _coroutineRunner.StopCoroutine(ref _takePizzaCoroutine);
            _isWorking = false;
        }
    }
}
