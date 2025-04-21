using UnityEngine;

namespace MainGame.Scripts.Logic
{
    public class ObjectHoldPoint : MonoBehaviour
    {
        [field: SerializeField] public Transform Transform { get; private set; }
    }
}