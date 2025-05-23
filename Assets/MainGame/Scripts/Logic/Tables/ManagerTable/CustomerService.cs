using System;
using System.Collections;
using MainGame.Scripts.Infrastructure;
using MainGame.Scripts.Infrastructure.Services;
using MainGame.Scripts.Logic.Npc;
using UnityEngine;

namespace MainGame.Scripts.Logic.Tables.ManagerTable
{
    public class CustomerService
    {
        private readonly bool _isWorking = true;
        private readonly WaitUntil _waitHasUnServCustomer;
        private readonly WaitForSeconds _delayBetweenTakePizza;
        private readonly Coroutine _servCoroutine;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly ManagerObserver _managerObserver;
        private readonly CustomerObserver _customerObserver;
        private readonly TablePizzaTaker _tablePizzaTaker;
        private readonly ObjectHoldPoint _coinPoint;
        private readonly CoinPayer _coinPayer;

        public CustomerService(ManagerObserver managerObserver, CustomerObserver customerObserver,
            TablePizzaTaker tablePizzaTaker, float delayBetweenTakePizza, ObjectHoldPoint coinPoint)
        {
            _coroutineRunner = AllServices.Container.Single<ICoroutineRunner>();
            _managerObserver = managerObserver;
            _customerObserver = customerObserver;
            _tablePizzaTaker = tablePizzaTaker;
            _coinPoint = coinPoint;
            _coinPayer = new CoinPayer(coinPoint);

            _delayBetweenTakePizza = new WaitForSeconds(delayBetweenTakePizza);
            _waitHasUnServCustomer = new WaitUntil(() => _customerObserver.Customer != null);

            _coroutineRunner.StopCoroutine(ref _servCoroutine);

            _servCoroutine = _coroutineRunner.StartCoroutine(ServCustomers());
        }

        private IEnumerator ServCustomers()
        {
            while (_isWorking)
            {
                yield return _waitHasUnServCustomer;
                
                Customer customer = _customerObserver.Customer;
                
                while (customer.PizzaTaker.CountWantedPizza > 0)
                {
                    if (_managerObserver.HasManager && _tablePizzaTaker.HasObjects)
                    {
                        customer.PizzaTaker.TakePizza(_tablePizzaTaker.GetObject());
                    }
                    
                    yield return _delayBetweenTakePizza;
                }
                
                customer.FinalizeService();
                _coinPayer.SpawnCoins(customer.PizzaTaker.CountOfGotPizzas * 10);
            }
        }
    }
}