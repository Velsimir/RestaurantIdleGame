using System;
using MainGame.Scripts.Infrastructure.Services.ObjectSpawner;
using MainGame.Scripts.Logic.PlayerLogic.Animations;
using Pathfinding;
using UnityEngine;

namespace MainGame.Scripts.Logic.Npc
{
    [RequireComponent(typeof(CharacterController))]
    public class Customer : MonoBehaviour, ISpawnable
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private PizzaHoldPoint _pizzaHoldPint;
        [SerializeField] private AIPath _aiPath;
        [SerializeField] private Seeker _seeker;
        [SerializeField] private Animator _animator;
        
        private CustomerAnimator _customerAnimator;
        private Pizza _currentPizza;
        
        public event Action<ISpawnable> Disappeared;
        
        public bool IsServed { get; private set; }

        private void Awake()
        {
            _customerAnimator = new CustomerAnimator(_animator, _aiPath);
        }

        private void Update()
        {
            _customerAnimator.Update(Time.deltaTime);
        }

        public void Disappear()
        {
            IsServed = false;
            _transform.gameObject.SetActive(false);
            _currentPizza.Disappear();
            Disappeared?.Invoke(this);
        }

        public void TakePizza(Pizza pizza)
        {
            _currentPizza = pizza;
            _currentPizza.SetParent(_transform);
            _currentPizza.transform.rotation = Quaternion.identity;
            _currentPizza.transform.position = _pizzaHoldPint.transform.position;
            IsServed = true;
        }

        public void TakeDestination(Transform point)
        {
            _seeker.StartPath(_transform.position, point.position);
        }
    }
}