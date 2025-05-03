using System.Collections.Generic;
using MainGame.Scripts.Logic.Npc;
using MainGame.Scripts.Logic.Tables.ManagerTable;
using UnityEngine;

namespace MainGame.Scripts.Logic.Concierge
{
    public class CustomerConcierge : MonoBehaviour
    {
        [Header("Настройки")]
        [SerializeField] private float _fillFactor = 0.4f;
        [SerializeField] private float _fillingRate = 0.1f;
        [SerializeField] private QueuePositionHandler _queuePositionHandler;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _exit;
    
        private readonly List<Customer> _customers = new List<Customer>();
    
        private float _timeSinceLastInvite;
        private CustomerInviter _customerInviter;

        private int CurrentQueueSize => _customers.Count;
        private int MaxFreeSeats => _queuePositionHandler.CountPositions;

        private void Awake()
        {
            _customerInviter = new CustomerInviter(_spawnPoint);
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
            if (_queuePositionHandler.TryGetQueuePosition(out  QueuePosition queuePosition))
            {
                Customer customer = _customerInviter.Invite(queuePosition);
                customer.Serviced += SendCustomerToNextPoint;
                _customers.Add(customer);
            }
        }

        private void SendCustomerToNextPoint(Customer customer)
        {
            // TODO нужно будет добавить столы, куда будут садится посетители, пока отправляются просто на выход
            customer.Serviced -= SendCustomerToNextPoint;
            customer.TakeDestination(_exit);
            _customers.Remove(customer);
            _queuePositionHandler.UnReserveLastQueuePlace();
            MoveQueue();
        }

        private void MoveQueue()
        {
            for (int i  = 0; i < _customers.Count; i++)
            {
                QueuePosition position = _queuePositionHandler.GetPosition(i);
                
                _customers[i].TakeDestination(position.transform);
            }
        }
    }
}
