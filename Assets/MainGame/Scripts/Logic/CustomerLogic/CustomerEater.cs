using System;
using System.Collections;
using UnityEngine;

namespace MainGame.Scripts.Logic.Npc
{
    public class CustomerEater : MonoBehaviour
    {
        [SerializeField] private float _timeToEat;

        private WaitForSeconds _waitDelayBetweenEat;
        
        public event Action AllPizzasEated;
        
        private void Awake()
        {
            _waitDelayBetweenEat = new WaitForSeconds(_timeToEat);
        }

        public void Eat(CustomerPizzaTaker pizzaTaker)
        {
            StartCoroutine(EatAllPizzas(pizzaTaker));
        }

        private IEnumerator EatAllPizzas(CustomerPizzaTaker pizzaTaker)
        {
            while (pizzaTaker.CountOfGotPizzas > 0)
            {
                pizzaTaker.GetPizza().Disappear();
                
                yield return _waitDelayBetweenEat;
            }
            
            AllPizzasEated?.Invoke();
        }
    }
}