using MainGame.Scripts.Infrastructure.Factory;
using MainGame.Scripts.Infrastructure.Services;
using UnityEngine;

namespace MainGame.Scripts.Logic.Npc
{
    public class NpcSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _tableCustomerTrigger;
        
        private IGameFactory _factory;
        private Customer _customer;

        private void Awake()
        {
            _factory = AllServices.Container.Single<IGameFactory>();
        }

        [ContextMenu("Spawn Npc")]
        private void SpawnCustomer()
        {
            _customer = _factory.CreateCustomer();
            _customer.transform.position = transform.position;
            _customer.TakeDestination(_tableCustomerTrigger);
        }
    }
}