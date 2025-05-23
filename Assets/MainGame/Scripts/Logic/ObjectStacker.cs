using System.Collections.Generic;
using UnityEngine;

namespace MainGame.Scripts.Logic
{
    public class ObjectStacker
    {
        public Vector3 GetSpawnPoint(Stack<ITakable> objects, Transform _spawnPoint)
        {
            if (objects.Count > 0)
            {
                float yPosition = CalculateYPosition(objects);
                return new Vector3(_spawnPoint.transform.position.x, yPosition, _spawnPoint.transform.position.z);
            }
            else
            {
                return _spawnPoint.position;
            }
        }

        private float CalculateYPosition(Stack<ITakable> pizzas)
        {
            return pizzas.Peek().Collider.bounds.max.y;
        }
    }
}