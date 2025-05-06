using System.Collections.Generic;
using MainGame.Scripts.Logic.Npc;
using UnityEngine;

namespace MainGame.Scripts.Logic.Tables.EatTableLogic
{
    public class FoodTable : MonoBehaviour, IActivatable
    {
        [SerializeField] private List<EatPlace> _allEatPlaces;
        
        private Stack<EatPlace> _freePlacesToEat;

        public bool IsActivated { get; private set; } = false;
        public bool HasFreePlace => _freePlacesToEat.Count > 0;

        private void Awake()
        {
            _freePlacesToEat = new Stack<EatPlace>();

            foreach (EatPlace eatPlace in _allEatPlaces)
            {
                _freePlacesToEat.Push(eatPlace);
            }

            IsActivated = true;
        }

        public void Activate()
        {
            IsActivated = true;
            gameObject.SetActive(true);
        }

        public void SendCustomerToReserve(Customer customer)
        {
            EatPlace placeToEat = _freePlacesToEat.Pop();
            placeToEat.Reserve(customer);
            placeToEat.Vacated += AddFreePlace;
        }

        private void AddFreePlace(EatPlace eatPlace)
        {
            eatPlace.Vacated -= AddFreePlace;
            _freePlacesToEat.Push(eatPlace);
        }
    }
}