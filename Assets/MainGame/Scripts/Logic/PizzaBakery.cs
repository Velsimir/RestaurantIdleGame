using System.Collections;
using System.Collections.Generic;
using MainGame.Scripts.Infrastructure.Factory;
using MainGame.Scripts.Infrastructure.Services;
using UnityEngine;

public class PizzaBakery : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private int _maxCountPizza;
    [SerializeField] private float _spawnDelay;

    private WaitForSeconds _wait;
    private Stack<Pizza> _pizzas;
    private IGameFactory _gameFactory;

    private void Awake()
    {
        _pizzas = new Stack<Pizza>();
        _wait = new WaitForSeconds(_spawnDelay);
        _gameFactory = AllServices.Container.Single<IGameFactory>();

        StartCoroutine(SpawnProcess());
    }

    [ContextMenu("DeactivatePizza")]
    public void DeactivatePizza()
    {
        Pizza pizza = _pizzas.Pop() as Pizza;
        
        pizza.gameObject.SetActive(false);
    }
    
    [ContextMenu("StartSpawn")]
    public void StartSpawn()
    {
        StartCoroutine(SpawnProcess());
    }

    private IEnumerator SpawnProcess()
    {
        while (_pizzas.Count <= _maxCountPizza)
        {
            yield return _wait;

            SpawnPizza();
        }
    }

    private void SpawnPizza()
    {
        Pizza pizza = _gameFactory.CreatePizza(_spawnPoint);
        pizza.transform.position = GetSpawnPoint();
        _pizzas.Push(pizza);
    }

    private Vector3 GetSpawnPoint()
    {
        if (_pizzas.Count > 0)
        {
            float yPosition = CalculateYPosition();
            return new Vector3(_spawnPoint.transform.position.x, yPosition, _spawnPoint.transform.position.z);
        }
        else
        {
            return _spawnPoint.position;
        }
    }

    private float CalculateYPosition()
    {
        Pizza pizza = _pizzas.Peek();
        float pizzaSize = pizza.GetBounds().max.y;
        return pizzaSize;
    }
}