using System.Collections.Generic;
using MainGame.Scripts.Infrastructure.Services.ObjectSpawner;
using UnityEngine;

namespace MainGame.Scripts.Logic
{
    public class ObjectStacker<T> where T : IStackable
    {
        public Vector3 GetSpawnPoint(Stack<T> pizzas, Transform _spawnPoint)
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

        private float CalculateYPosition(Stack<T> pizzas)
        {
            return pizzas.Peek().Collider.bounds.max.y + 0.1f;
        }
    }
}