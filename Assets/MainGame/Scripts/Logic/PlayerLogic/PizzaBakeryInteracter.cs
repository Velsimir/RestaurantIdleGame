using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainGame.Scripts.Logic.PlayerLogic
{
    public class PizzaBakeryInteracter : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private Transform _holdPizzaPoint;
        [SerializeField] private Transform _transform;
        [SerializeField] private float _delay = 0.2f;

        private bool _isInteracting = false;
        private WaitForSeconds _waitDelay;
        private Coroutine _pizzaBakeryCoroutine;
        private Stack<Pizza> _pizzas = new Stack<Pizza>();

        private void Awake()
        {
            _waitDelay = new WaitForSeconds(_delay);
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
                if (pizzaBakery.HasPizza)
                {
                    GetPizza(pizzaBakery.GetPizza());
                }

                yield return _waitDelay;
            }
        }

        private void GetPizza(Pizza pizza)
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
