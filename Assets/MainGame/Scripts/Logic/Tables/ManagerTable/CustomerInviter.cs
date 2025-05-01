using System.Collections;
using System.Collections.Generic;
using MainGame.Scripts.Infrastructure.Factory;
using MainGame.Scripts.Infrastructure.Services;
using MainGame.Scripts.Logic.Npc;
using UnityEngine;

namespace MainGame.Scripts.Logic.Tables.ManagerTable
{
    public class CustomerInviter : MonoBehaviour
    {
        [SerializeField] private float _inviteDelay;
        [SerializeField] private QueuePositionHandler _queuePositionHandler;
        
        private IGameFactory _factory;
        private List<Customer> _customers;
        private WaitForSeconds _delay;
        private bool _isWorking = true;
        private Coroutine _servCoroutine;

        private void Awake()
        {
            _customers = new List<Customer>();
            _factory = AllServices.Container.Single<IGameFactory>();
            _delay = new WaitForSeconds(_inviteDelay);

            StopCurrentCoroutine();

            _servCoroutine = StartCoroutine(InviteCustomers());
        }

        private void Invite(QueuePosition queuePosition)
        {
            Customer customer = _factory.CreateCustomer();
            customer.TakeDestination(queuePosition.transform);
            _customers.Add(customer);
        }

        private IEnumerator InviteCustomers()
        {
            while (_isWorking)
            {
                if (_queuePositionHandler.TryGetQueuePosition(out QueuePosition queuePosition))
                {
                    Invite(queuePosition);
                }
                
                yield return _delay;
            }
        }

        private void StopCurrentCoroutine()
        {
            if (_servCoroutine != null)
            {
                StopCoroutine(_servCoroutine);
            }
        }
    }
}