using System.Collections.Generic;
using UnityEngine;

namespace MainGame.Scripts.Logic
{
    public class PizzaStacker
    {
        public Vector3 GetSpawnPoint(Stack<Pizza> pizzas, Transform _spawnPoint)
        {
            if (pizzas.Count > 0)
            {
                float yPosition = CalculateYPosition(pizzas);
                return new Vector3(_spawnPoint.transform.position.x, yPosition, _spawnPoint.transform.position.z);
            }
            else
            {
                return _spawnPoint.position;
            }
        }

        private float CalculateYPosition(Stack<Pizza> pizzas)
        {
            return pizzas.Peek().Bounds.max.y;
        }
    }
}