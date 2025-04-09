using UnityEngine;

namespace MainGame.Scripts.Logic.Npc
{
    public class NpcRouterProvider : MonoBehaviour
    {
        public void SendNpcToPoint(Customer customer, Transform target)
        {
            customer.TakeDestanation(target);
        }
    }
}