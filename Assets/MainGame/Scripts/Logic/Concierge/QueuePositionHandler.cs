using System.Collections.Generic;
using UnityEngine;

namespace MainGame.Scripts.Logic.Concierge
{
    public class QueuePositionHandler : MonoBehaviour
    {
        [SerializeField] private List<QueuePosition> _customerQueuePositions = new List<QueuePosition>();

        public int CountPositions => _customerQueuePositions.Count;

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
        
        public QueuePosition GetPosition(int index)
        {
            return _customerQueuePositions[index];
        }

        public void UnReserveLastQueuePlace()
        {
            QueuePosition lastQueuePosition = _customerQueuePositions[_customerQueuePositions.Count - 1];
            
            foreach (var queuePosition in _customerQueuePositions)
            {
                if (queuePosition.IsReserved == true)
                {
                    lastQueuePosition = queuePosition;
                }
            }
            
            lastQueuePosition.UnReserve();
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