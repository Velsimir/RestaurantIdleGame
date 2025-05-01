using System.Collections.Generic;
using UnityEngine;

namespace MainGame.Scripts.Logic
{
    public class QueuePositionHandler : MonoBehaviour
    {
        [SerializeField] private List<QueuePosition> _customerQueuePositions = new List<QueuePosition>();

        private void Awake()
        {
            UnReserveAllPositions();
        }

        public bool TryGetQueuePosition(out QueuePosition queuePosition)
        {
            foreach (var position in _customerQueuePositions)
            {
                if (position.IsReserved == false)
                {
                    queuePosition = position;
                    queuePosition.Reserve();
                    return true;
                }
            }
            
            queuePosition = null;
            return false;
        }

        private void UnReserveAllPositions()
        {
            foreach (var queuePosition in _customerQueuePositions)
            {
                queuePosition.UnReserve();
            }
        }
    }
}