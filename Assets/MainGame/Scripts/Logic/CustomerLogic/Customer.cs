using System;
using MainGame.Scripts.Infrastructure.Services.ObjectSpawner;
using MainGame.Scripts.Logic.PlayerLogic.Animations;
using MainGame.Scripts.Logic.Tables.EatTableLogic;
using Pathfinding;
using UnityEngine;

namespace MainGame.Scripts.Logic.Npc
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(AIPath))]
    [RequireComponent(typeof(Seeker))]
    public class Customer : MonoBehaviour, ISpawnable
    {
        [SerializeField] private CustomerPizzaTaker _pizzaTaker;
        [SerializeField] private CustomerEater _eater;
        [SerializeField] private Transform _transform;
        [SerializeField] private AIPath _aiPath;
        [SerializeField] private Seeker _seeker;
        [SerializeField] private Animator _animator;

        private CustomerAnimator _customerAnimator;

        public event Action<ISpawnable> Disappeared;
        public event Action<Customer> Serviced;
        public event Action<Customer> FinishedFood;
        
        public CustomerPizzaTaker PizzaTaker => _pizzaTaker;

        private void Awake()
        {
            _customerAnimator = new CustomerAnimator(_animator, _aiPath);
        }

        private void OnEnable()
        {
            _eater.AllPizzasEated += FinishFood;
        }

        private void Update()
        {
            _customerAnimator.Update(Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EatPlace eatPlace))
            {
                _eater.Eat(_pizzaTaker);
            }
        }

        public void Disappear()
        {
            _transform.gameObject.SetActive(false);

            Disappeared?.Invoke(this);
        }

        public void TakeDestination(Transform point)
        {
            _seeker.StartPath(_transform.position, point.position);
        }

        public void FinalizeService()
        {
            Serviced?.Invoke(this);
        }

        private void FinishFood()
        {
            FinishedFood?.Invoke(this);
        }
    }
}