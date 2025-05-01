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
        private readonly float _delay = 0.1f;
        private readonly bool _isWorking = true;
        private readonly WaitUntil _waitHasUnServCustomer;
        private readonly WaitForSeconds _wait;
        private readonly Coroutine _servCoroutine;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly ManagerObserver _managerObserver;
        private readonly CustomerObserver _customerObserver;
        private readonly TablePizzaTaker _tablePizzaTaker;

        public CustomerService(ManagerObserver managerObserver, CustomerObserver customerObserver, TablePizzaTaker tablePizzaTaker)
        {
            _coroutineRunner = AllServices.Container.Single<ICoroutineRunner>();
            _managerObserver = managerObserver;
            _customerObserver = customerObserver;
            _tablePizzaTaker = tablePizzaTaker;
            
            _waitHasUnServCustomer = new WaitUntil(() 
                => _customerObserver.Customer != null);

            _wait = new WaitForSeconds(_delay);

            _coroutineRunner.StopCoroutine(ref _servCoroutine);

            _servCoroutine = _coroutineRunner.StartCoroutine(ServCustomers());
        }

        public event Action CustomerServed;

        private IEnumerator ServCustomers()
        {
            while (_isWorking)
            {
                yield return _waitHasUnServCustomer;
            
                if (_managerObserver.HasManager && _tablePizzaTaker.HasPizzas)
                {
                    Customer customer = _customerObserver.Customer;
                    
                    customer.TakePizza(_tablePizzaTaker.GetPizza());
                    
                    CustomerServed?.Invoke();
                }

                yield return _wait;
            }
        }
    }
}