using System;
using MainGame.Scripts.Logic.Npc;
using UnityEngine;

namespace MainGame.Scripts.Logic.Tables.ManagerTable
{
    public class CustomerObserver : IDisposable
    {
        private readonly TriggerObserver _customerObserver;
        private CustomerService _customerService;

        public CustomerObserver(TriggerObserver customerObserver)
        {
            _customerObserver = customerObserver;
            
            _customerObserver.CollusionEntered += TakeCustomer;
        }

        public Customer Customer { get; private set; }

        public void Dispose()
        {
            _customerObserver.CollusionEntered -= TakeCustomer;
        }

        private void TakeCustomer(Collider collider)
        {
            if (collider.TryGetComponent(out Customer customer))
            {
                Customer = customer;
                Customer.Serviced += UnTakeCustomer;
            }
        }

        private void UnTakeCustomer(Customer customer)
        {
            Customer.Serviced -= UnTakeCustomer;
            Customer = null;
        }
    }
}