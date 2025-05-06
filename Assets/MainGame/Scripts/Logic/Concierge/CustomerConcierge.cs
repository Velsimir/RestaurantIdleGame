using System.Collections;
using System.Collections.Generic;
using MainGame.Scripts.Logic.Npc;
using MainGame.Scripts.Logic.Tables.EatTableLogic;
using MainGame.Scripts.Logic.Tables.ManagerTable;
using UnityEngine;

namespace MainGame.Scripts.Logic.Concierge
{
    public class CustomerConcierge : MonoBehaviour
    {
        [SerializeField] private float _fillFactor = 0.4f;
        [SerializeField] private float _fillingRate = 0.1f;
        [SerializeField] private QueuePositionHandler _queuePositionHandler;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _exit;
        [SerializeField] private List<FoodTable> _tablesToEat;

        private readonly List<Customer> _customers = new List<Customer>();

        private float _timeSinceLastInvite;
        private CustomerInviter _customerInviter;
        private FoodTable _freeTable;
        private WaitForSeconds _waitForSeconds;

        private bool HasFreeTable
        {
            get
            {
                foreach (var table in _tablesToEat)
                {
                    if (table.IsActivated && table.HasFreePlace)
                    {
                        _freeTable = table;
                        return true;
                    }
                }

                return false;
            }
        }
        private int CurrentQueueSize => _customers.Count;
        private int MaxFreeSeats => _queuePositionHandler.CountPositions;

        private void Awake()
        {
            _customerInviter = new CustomerInviter(_spawnPoint);
            _waitForSeconds = new WaitForSeconds(1);
        }

        private void Update()
        {
            _timeSinceLastInvite += Time.deltaTime;

            if (CheckTimerToInviteCustomer())
            {
                InviteGuest();
                _timeSinceLastInvite = 0f;
            }
        }

        private bool CheckTimerToInviteCustomer()
        {
            float targetQueueSize = _fillFactor * MaxFreeSeats * (1 - Mathf.Exp(-_fillingRate * _timeSinceLastInvite));

            return CurrentQueueSize < targetQueueSize;
        }

        private void InviteGuest()
        {
            if (_queuePositionHandler.TryGetQueuePosition(out QueuePosition queuePosition))
            {
                Customer customer = _customerInviter.Invite(queuePosition);
                customer.Serviced += SendCustomerToNextPoint;
                _customers.Add(customer);
            }
        }

        private void SendCustomerToNextPoint(Customer customer)
        {
            customer.Serviced -= SendCustomerToNextPoint;

            StartCoroutine(SendCustomerToEatTable(customer));
        }

        private IEnumerator SendCustomerToEatTable(Customer customer)
        {
            while (HasFreeTable == false)
            {
                yield return _waitForSeconds;
            }

            _freeTable.SendCustomerToReserve(customer);
            _customers.Remove(customer);
            
            customer.FinishedFood += SendCustomerToExit;
            
            _queuePositionHandler.UnReserveLastQueuePlace();
            
            MoveQueue();
        }

        private void SendCustomerToExit(Customer customer)
        {
            customer.TakeDestination(_exit);
        }

        private void MoveQueue()
        {
            for (int i = 0; i < _customers.Count; i++)
            {
                QueuePosition position = _queuePositionHandler.GetPosition(i);

                _customers[i].TakeDestination(position.transform);
            }
        }
    }
}