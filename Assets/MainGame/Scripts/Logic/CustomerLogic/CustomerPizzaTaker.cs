using System.Collections.Generic;
using UnityEngine;

namespace MainGame.Scripts.Logic.Npc
{
    public class CustomerPizzaTaker : MonoBehaviour
    {
        [SerializeField] private ObjectHoldPoint _objectHoldPint;
        [SerializeField] private Transform _transform;
        
        private Stack<Pizza> _pizzas;
        private ObjectStacker<Pizza> _objectStacker;
        
        public int CountWantedPizza { get; private set; }
        public int CountOfGotPizzas => _pizzas.Count;
        
        private void Awake()
        {
            _pizzas = new Stack<Pizza>();
            _objectStacker = new ObjectStacker<Pizza>();
        }
        
        private void OnEnable()
        {
            CountWantedPizza = Random.Range(1, 3);
        }

        public Pizza GetPizza()
        {
            return _pizzas.Pop();
        }

        public void TakePizza(Pizza pizza)
        {
            pizza.transform.position = _objectStacker.GetSpawnPoint(_pizzas, _objectHoldPint.Transform);
            pizza.SetParent(_transform);
            pizza.transform.localRotation = Quaternion.identity;
            _pizzas.Push(pizza);
            CountWantedPizza--;
        }
    }
}