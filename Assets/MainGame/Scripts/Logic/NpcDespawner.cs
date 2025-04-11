using MainGame.Scripts.Logic.Npc;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class NpcDespawner : MonoBehaviour
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
