using UnityEngine;

namespace MainGame.Scripts.Infrastructure.Services.ObjectSpawner
{
    public interface ISpawnerService<TSpawnableObjet> where TSpawnableObjet : MonoBehaviour, ISpawnable
    {
        TSpawnableObjet Spawn();
        TSpawnableObjet Spawn(Transform at);
    }
}