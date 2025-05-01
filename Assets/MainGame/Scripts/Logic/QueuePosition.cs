using UnityEngine;

namespace MainGame.Scripts.Logic
{
    public class QueuePosition : MonoBehaviour
    {
        public bool IsReserved { get; private set; }

        public void Reserve()
        {
            IsReserved = true;
        }

        public void UnReserve()
        {
            IsReserved = false;
        }
    }
}