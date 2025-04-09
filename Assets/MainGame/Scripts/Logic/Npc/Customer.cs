using System;
using MainGame.Scripts.Infrastructure.Services.ObjectSpawner;
using UnityEngine;

namespace MainGame.Scripts.Logic.Npc
{
    [RequireComponent(typeof(CharacterController))]
    public class Customer : MonoBehaviour, ISpawnable
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Transform _pizzaHoldPint;
        [SerializeField] private CharacterController _characterController;
        
        public event Action<ISpawnable> Disappeared;
        public event Action EatEnded;
        
        public bool IsServed { get; set; }

        public void Disappear()
        {
            IsServed = false;
            _transform.gameObject.SetActive(false);
            Disappeared?.Invoke(this);
        }

        private void Eat()
        {
            EatEnded?.Invoke();
        }

        public void TakePizza(Pizza pizza)
        {
            pizza.SetParent(_transform);
            pizza.transform.rotation = Quaternion.identity;
            pizza.transform.position = _pizzaHoldPint.position;
            IsServed = true;
        }

        public void TakeDestanation(Transform point)
        {
            
        }
    }
}