using System;
using UnityEngine;

namespace MainGame.Scripts.Logic.Tables.ManagerTable
{
    public class ReceptionTable : MonoBehaviour
    {
        [SerializeField] private ObjectHoldPoint _holdPizzaPoint;
        [SerializeField] private TriggerObserver _triggerPizzaObserver;
        [SerializeField] private TriggerObserver _triggerManagerObserver;
        [SerializeField] private TriggerObserver _triggerCustomerObserver;
        [SerializeField] private ObjectHoldPoint _coinPoint;
        [SerializeField] private int _maxPizzasOnTable;
        [SerializeField] private float _delayToSetPizzas;
        [SerializeField] private float _delayBetweenTakePizza;

        private TablePizzaTaker _tablePizzaTaker;
        private ManagerObserver _managerObserver;
        private CustomerObserver _customerObserver;
        private CustomerService _customerService;
        
        public event Action<Collider> TargetEntered;
        public event Action<Collider> TargetExited;

        public Transform Transform { get; private set; }
        public ObjectHoldPoint HoldPizzaPoint => _holdPizzaPoint;

        private void Awake()
        {
            Transform = transform;
            _tablePizzaTaker = new TablePizzaTaker(this, _maxPizzasOnTable, _delayToSetPizzas);
            _managerObserver = new ManagerObserver(_triggerManagerObserver);
            _customerObserver = new CustomerObserver(_triggerCustomerObserver);
            _customerService = new CustomerService(_managerObserver, _customerObserver, _tablePizzaTaker, _delayBetweenTakePizza, _coinPoint);
        }

        private void OnEnable()
        {
            _triggerPizzaObserver.CollusionEntered += HandleEnteredTarget;
            _triggerPizzaObserver.CollusionExited += HandleExitedTarget;
            
            _triggerManagerObserver.CollusionEntered += HandleEnteredTarget;
            _triggerManagerObserver.CollusionExited += HandleExitedTarget;
            
            _triggerCustomerObserver.CollusionEntered += HandleEnteredTarget;
            _triggerCustomerObserver.CollusionExited += HandleExitedTarget;
        }

        private void OnDisable()
        {
            _triggerPizzaObserver.CollusionEntered -= HandleEnteredTarget;
            _triggerPizzaObserver.CollusionExited -= HandleExitedTarget;
            
            _triggerManagerObserver.CollusionEntered -= HandleEnteredTarget;
            _triggerManagerObserver.CollusionExited -= HandleExitedTarget;
            
            _triggerCustomerObserver.CollusionEntered -= HandleEnteredTarget;
            _triggerCustomerObserver.CollusionExited -= HandleExitedTarget;
            
            _tablePizzaTaker.Dispose();
            _managerObserver.Dispose();
            _customerObserver.Dispose();
        }

        private void HandleEnteredTarget(Collider obj)
        {
            TargetEntered?.Invoke(obj);
        }

        private void HandleExitedTarget(Collider obj)
        {
            TargetExited?.Invoke(obj);
        }
    }
}