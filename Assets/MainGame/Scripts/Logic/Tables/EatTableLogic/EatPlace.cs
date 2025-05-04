using System;
using MainGame.Scripts.Logic.Npc;
using UnityEngine;

namespace MainGame.Scripts.Logic.Tables.EatTableLogic
{
    public class EatPlace : MonoBehaviour
    {
        private Customer _currentCustomer;
        
        public event Action<EatPlace> Vacated;
        
        public bool IsFree { get; private set; } = true;

        public void Reserve(Customer customer)
        {
            IsFree = false;
            _currentCustomer = customer;
            _currentCustomer.TakeDestination(this.transform);
            _currentCustomer.FinishedFood += Unreserve;
        }

        private void Unreserve(Customer customer)
        {
            IsFree = true;
            _currentCustomer.FinishedFood -= Unreserve;
            
            Vacated?.Invoke(this);
        }
    }
}