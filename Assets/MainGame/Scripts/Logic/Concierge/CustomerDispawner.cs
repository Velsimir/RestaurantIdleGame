using MainGame.Scripts.Logic.Npc;
using UnityEngine;

namespace MainGame.Scripts.Logic.Concierge
{
    [RequireComponent(typeof(Collider))]
    public class CustomerDispawner : MonoBehaviour
    {
        [SerializeField] private Collider _collider;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Customer customer))
            {
                customer.Disappear();
                customer.transform.position = Vector3.zero;
            }
        }
    }
}
