using System;
using System.Collections.Generic;
using MainGame.Scripts.Infrastructure.Services.ObjectSpawner;
using MainGame.Scripts.Logic.PlayerLogic.Animations;
using Pathfinding;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MainGame.Scripts.Logic.Npc
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(AIPath))]
    [RequireComponent(typeof(Seeker))]
    public class Customer : MonoBehaviour, ISpawnable
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private ObjectHoldPoint _objectHoldPint;
        [SerializeField] private AIPath _aiPath;
        [SerializeField] private Seeker _seeker;
        [SerializeField] private Animator _animator;
        
        private CustomerAnimator _customerAnimator;
        private Stack<Pizza> _pizzas;
        private PizzaStacker _pizzaStacker;
        
        public event Action<ISpawnable> Disappeared;
        public event Action<Customer> Serviced;

        public int CountWantedPizza { get; private set; }

        private void Awake()
        {
            _pizzas = new Stack<Pizza>();
            _customerAnimator = new CustomerAnimator(_animator, _aiPath);
            _pizzaStacker = new PizzaStacker();
        }

        private void OnEnable()
        {
            CountWantedPizza = Random.Range(1, 3);
        }

        private void Update()
        {
            _customerAnimator.Update(Time.deltaTime);
        }

        public void Disappear()
        {
            _transform.gameObject.SetActive(false);
            foreach (var pizza in _pizzas)
            {
                pizza.Disappear();
            }
            Disappeared?.Invoke(this);
        }

        public void TakePizza(Pizza pizza)
        {
            pizza.transform.position = _pizzaStacker.GetSpawnPoint(_pizzas, _objectHoldPint.Transform);
            pizza.SetParent(_transform);
            pizza.transform.localRotation = Quaternion.identity;
            _pizzas.Push(pizza);
            CountWantedPizza--;
        }

        public void TakeDestination(Transform point)
        {
            _seeker.StartPath(_transform.position, point.position);
        }

        public void FinalizeService()
        {
            Serviced?.Invoke(this);
        }
    }
}