using UnityEngine;

namespace MainGame.Scripts.Infrastructure.AssetManagment
{
    public interface IAssetProvider
    {
        GameObject Instantiate(string path, Vector3 at);
        GameObject Instantiate(string path);
    }
}