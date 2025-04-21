using UnityEngine;

namespace MainGame.Scripts.Infrastructure.AssetManagment
{
    public class Asset : IAsset
    {
        public TPrefab GetPrefab<TPrefab>(string path) where TPrefab : MonoBehaviour
        {
            return Resources.Load<TPrefab>(path);
        }

        public GameObject Instantiate(string path, Vector3 at)
        {
            GameObject playerPrefab = Resources.Load<GameObject>(path);
            
            return Object.Instantiate(playerPrefab, at, Quaternion.identity); 
        }

        public GameObject Instantiate(string path)
        {
            GameObject playerPrefab = Resources.Load<GameObject>(path);
            
            return Object.Instantiate(playerPrefab); 
        }

        public TData GetStaticData<TData>(string path) where TData : ScriptableObject
        {
            return Resources.Load<TData>(path);
        }
    }
}