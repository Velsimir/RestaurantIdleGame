using System.Collections;
using System.Collections.Generic;
using MainGame.Scripts.Infrastructure.Services;
using MainGame.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace MainGame.Scripts.Logic.PlayerLogic
{
    public class PizzaTaker : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private Transform _holdPizzaPoint;
        [SerializeField] private Transform _transform;
        [SerializeField] private float _delay = 0.2f;

        private bool _isInteracting = false;
        private WaitForSeconds _waitDelay;
        private Coroutine _pizzaBakeryCoroutine;
        private Stack<Pizza> _pizzas = new Stack<Pizza>();
        private int _maxPizzas;
        
        public bool HasPizza => _pizzas.Count > 0;

        private void Awake()
        {
            _waitDelay = new WaitForSeconds(_delay);
            _maxPizzas = AllServices.Container.Single<IPersistentProgressService>().Progress.MaxPizzaHoldCount;
        }

        private void OnEnable()
        {
            _triggerObserver.CollusionEntered += TryInteract;
            _triggerObserver.CollusionExited += DeInteract;
        }

        private void OnDisable()
        {
            _triggerObserver.CollusionEntered -= TryInteract;
            _triggerObserver.CollusionExited -= DeInteract;
        }

        public Pizza GetPizza()
        {
            return _pizzas.Pop();
        }

        private void TryInteract(Collider collider)
        {
            _isInteracting = true;
            
            if (collider.transform.TryGetComponent(out PizzaBakery pizzaBakery))
            {
                StopCurrentCoroutine();

                _pizzaBakeryCoroutine = StartCoroutine(TakingPizza(pizzaBakery));
            }
        }

        private IEnumerator TakingPizza(PizzaBakery pizzaBakery)
        {
            while (_isInteracting)
            {
                if (pizzaBakery.HasPizza && _pizzas.Count < _maxPizzas)
                {
                    TakePizza(pizzaBakery.GetPizza());
                }

                yield return _waitDelay;
            }
        }

        private void TakePizza(Pizza pizza)
        {
            _pizzas.Push(pizza);
            SetNewPizzaPosition(pizza);
        }

        private void SetNewPizzaPosition(Pizza pizza)
        {
            pizza.SetParent(_transform);
            pizza.transform.localRotation = Quaternion.identity;
            pizza.transform.localPosition = GetHeightPosition(pizza.Bounds.size.y);
        }

        private Vector3 GetHeightPosition(float pizzaSize)
        {
            float heightOffset = _pizzas.Count * pizzaSize;

            return new Vector3(_holdPizzaPoint.localPosition.x, heightOffset, _holdPizzaPoint.localPosition.z);
        }

        private void DeInteract(Collider obj)
        {
            _isInteracting = false;
            
            StopCurrentCoroutine();
        }

        private void StopCurrentCoroutine()
        {
            if (_pizzaBakeryCoroutine != null)
            {
                StopCoroutine(_pizzaBakeryCoroutine);
                _pizzaBakeryCoroutine = null;
            }
        }
    }
}
