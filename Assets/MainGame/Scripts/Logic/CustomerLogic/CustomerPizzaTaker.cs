using System.Collections.Generic;
using UnityEngine;

namespace MainGame.Scripts.Logic.Npc
{
    public class CustomerPizzaTaker : MonoBehaviour
    {
        [SerializeField] private ObjectHoldPoint _objectHoldPint;
        [SerializeField] private Transform _transform;
        
        private Stack<ITakable> _pizzas;
        private ObjectStacker _objectStacker;
        
        public int CountWantedPizza { get; private set; }
        public int CountOfGotPizzas => _pizzas.Count;
        
        private void Awake()
        {
            _pizzas = new Stack<ITakable>();
            _objectStacker = new ObjectStacker();
        }
        
        private void OnEnable()
        {
            CountWantedPizza = Random.Range(1, 3);
        }

        public ITakable GetPizza()
        {
            return _pizzas.Pop();
        }

        public void TakePizza(ITakable pizza)
        {
            pizza.Take( _objectHoldPint.Transform,_objectStacker.GetSpawnPoint(_pizzas, _objectHoldPint.Transform));
            _pizzas.Push(pizza);
            CountWantedPizza--;
        }
    }
}