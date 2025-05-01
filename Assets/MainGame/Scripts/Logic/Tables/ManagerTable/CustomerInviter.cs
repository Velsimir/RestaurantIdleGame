using MainGame.Scripts.Infrastructure.Factory;
using MainGame.Scripts.Infrastructure.Services;
using MainGame.Scripts.Logic.Npc;
using UnityEngine;

namespace MainGame.Scripts.Logic.Tables.ManagerTable
{
    public class CustomerInviter
    {
        private readonly Transform _spawnPoint;
        private readonly IGameFactory _factory;
        private readonly bool _isWorking = true;
        private readonly Coroutine _servCoroutine;

        public CustomerInviter(Transform spawnPoint)
        {
            _spawnPoint = spawnPoint;
            _factory = AllServices.Container.Single<IGameFactory>();
        }

        public Customer Invite(QueuePosition queuePosition)
        {
            Customer customer = _factory.CreateCustomer(_spawnPoint);
            customer.TakeDestination(queuePosition.transform);
            
            return customer;
        }
    }
}