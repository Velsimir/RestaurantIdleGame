using MainGame.Scripts.Infrastructure.Services;
using UnityEngine;

namespace MainGame.Scripts.Infrastructure.AssetManagment
{
    public interface IAsset : IService
    {
        GameObject Instantiate(string path, Vector3 at);
        GameObject Instantiate(string path);
    }
}