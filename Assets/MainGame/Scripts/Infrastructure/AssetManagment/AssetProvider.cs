using UnityEngine;

namespace MainGame.Scripts.Infrastructure.AssetManagment
{
    public class AssetProvider
    {
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
    }
}