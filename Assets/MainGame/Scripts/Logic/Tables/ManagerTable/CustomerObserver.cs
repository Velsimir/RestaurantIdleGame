using System;
using MainGame.Scripts.Logic.Npc;
using UnityEngine;

namespace MainGame.Scripts.Logic.Tables.ManagerTable
{
    public class CustomerObserver : IDisposable
    {
        private readonly TriggerObserver _customerTrigger;
        private bool _hasCustomer;

        public CustomerObserver(TriggerObserver customerTrigger)
        {
            _customerTrigger = customerTrigger;
            
            _customerTrigger.CollusionEntered += TakeCustomer;
            _customerTrigger.CollusionExited += UnTakeCustomer;
        }

        public Customer Customer { get; private set; }
        public bool HasUnServCustomer => _hasCustomer && Customer.IsServed == false;
        
        public void Dispose()
        {
            _customerTrigger.CollusionEntered -= TakeCustomer;
            _customerTrigger.CollusionExited -= UnTakeCustomer;
        }

        private void TakeCustomer(Collider collider)
        {
            if (collider.TryGetComponent(out Customer customer))
            {
                Customer = customer;
                _hasCustomer = true;
            }
        }

        private void UnTakeCustomer(Collider collider)
        {
            if (collider.TryGetComponent(out Customer customer))
            {
                Customer = null;
                _hasCustomer = false;
            }
        }
    }
}