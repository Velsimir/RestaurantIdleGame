using MainGame.Scripts.Infrastructure.Services;
using MainGame.Scripts.Infrastructure.Services.ObjectSpawner;
using UnityEngine;

namespace MainGame.Scripts.Infrastructure.AssetManagment
{
    public interface IAsset : IService
    {
        GameObject Instantiate(string path, Vector3 at);
        GameObject Instantiate(string path);
        TPrefab GetPrefab<TPrefab>(string path) where TPrefab : MonoBehaviour;
    }
}