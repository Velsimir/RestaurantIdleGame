using UnityEngine;

namespace MainGame.Scripts.StaticData
{
    [CreateAssetMenu(menuName = "StaticData/Player", fileName = "PlayerStaticData")]
    public class PlayerStaticData : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }
    }
}