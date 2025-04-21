using System.Collections;
using System.Collections.Generic;
using MainGame.Scripts.Logic.PlayerLogic;
using UnityEngine;

namespace MainGame.Scripts.Logic.Tables
{
    public class ReceptionTablePizzaTaker : MonoBehaviour
    {
        [SerializeField] private ObjectHoldPoint _holdPizzaPoint;
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private Transform _transform;
        [SerializeField] private int _maxPizzas;
        [SerializeField] private float _delay;

        private Stack<Pizza> _pizzas = new Stack<Pizza>();
        private bool _isWorking = true;
        private Coroutine _takePizzaCoroutine;
        private WaitForSeconds _wait;
        private PizzaStacker _pizzaStacker;
    
        public bool HasPizzas => _pizzas.Count > 0;

        private void Awake()
        {
            _wait = new WaitForSeconds(_delay);
            _pizzaStacker = new PizzaStacker();
        }

        private void OnEnable()
        {
            _triggerObserver.CollusionEntered += TryTakePizzas;
            _triggerObserver.CollusionExited += StopWork;
        }
    
        private void OnDisable()
        {
            _triggerObserver.CollusionEntered -= TryTakePizzas;
            _triggerObserver.CollusionExited -= StopWork;
        }

        public Pizza GetPizza()
        {
            return _pizzas.Pop();
        }

        private void TryTakePizzas(Collider collider)
        {
            if (collider.transform.TryGetComponent(out PlayerPizzaTaker interactor))
            {
                _isWorking = true;
                StopCurrentCoroutine();
                _takePizzaCoroutine = StartCoroutine(TakePizzas(interactor));
            }
        }
    
        private IEnumerator TakePizzas(PlayerPizzaTaker interactor)
        {
            while (_isWorking)
            {
                if (interactor.HasPizza && _pizzas.Count <= _maxPizzas)
                {
                    TakePizza(interactor);
                }

                yield return _wait;
            }
        }

        private void TakePizza(PlayerPizzaTaker interactor)
        {
            Pizza pizza = interactor.GetPizza();
            pizza.transform.position = _pizzaStacker.GetSpawnPoint(_pizzas, _holdPizzaPoint.Transform);
            pizza.SetParent(_transform);
            _pizzas.Push(pizza);
        }

        private void StopWork(Collider collider)
        {
            StopCurrentCoroutine();
            _isWorking = false;
        }
    
        private void StopCurrentCoroutine()
        {
            if (_takePizzaCoroutine != null)
            {
                StopCoroutine(_takePizzaCoroutine);
                _takePizzaCoroutine = null;
            }
        }
    }
}
