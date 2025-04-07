using System.Collections;
using System.Collections.Generic;
using MainGame.Scripts.Logic;
using MainGame.Scripts.Logic.PlayerLogic;
using UnityEngine;

public class ReceptionTablePizzaTaker : MonoBehaviour
{
    
    [SerializeField] private Transform _holdPizzaPoint;
    [SerializeField] private TriggerObserver _triggerObserver;
    [SerializeField] private Transform _transform;
    [SerializeField] private int _maxPizzas;
    [SerializeField] private float _delay;

    private Stack<Pizza> _pizzas = new Stack<Pizza>();
    private bool _isWorking = true;
    private Coroutine _takePizzaCoroutine;
    private WaitForSeconds _wait;

    private void Awake()
    {
        _wait = new WaitForSeconds(_delay);
    }

    private void OnEnable()
    {
        _triggerObserver.CollusionEntered += TryTakePizzas;
        _triggerObserver.CollusionExited += StopSomeThing;
    }
    
    private void OnDisable()
    {
        _triggerObserver.CollusionEntered -= TryTakePizzas;
        _triggerObserver.CollusionExited -= StopSomeThing;
    }

    private void TryTakePizzas(Collider collider)
    {
        if (collider.transform.TryGetComponent(out PizzaBakeryInteractor interactor))
        {
            _isWorking = true;
            StopCurrentCoroutine();
            _takePizzaCoroutine = StartCoroutine(TakePizza(interactor));
        }
    }
    
    private IEnumerator TakePizza(PizzaBakeryInteractor interactor)
    {
        while (_isWorking)
        {
            if (interactor.HasPizza && _pizzas.Count <= _maxPizzas)
            {
                Pizza pizza = interactor.GetPizza();
                _pizzas.Push(pizza);
                SetNewPizzaPosition(pizza);
            }

            yield return _wait;
        }
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

        return new Vector3(_holdPizzaPoint.localPosition.x, _holdPizzaPoint.localPosition.y + heightOffset, _holdPizzaPoint.localPosition.z);
    }

    private void StopSomeThing(Collider collider)
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
