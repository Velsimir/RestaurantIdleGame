using System.Collections.Generic;
using UnityEngine;

namespace MainGame.Scripts.Infrastructure.Services.ObjectSpawner
{
    public class SpawnerService <TSpawnableObjet> : ISpawnerService <TSpawnableObjet> where TSpawnableObjet : MonoBehaviour, ISpawnable
    {
        private readonly TSpawnableObjet _spawnablePrefab;
        private readonly ObjectPoolService _poolService;

        public SpawnerService(TSpawnableObjet spawnablePrefab) 
        {
            _spawnablePrefab = spawnablePrefab;
            _poolService = new ObjectPoolService();
        }

        public TSpawnableObjet Spawn()
        {
            TSpawnableObjet spawnableObjet;

            if (_poolService.HasFree)
            {
                spawnableObjet = _poolService.Get();
            }
            else
            {
                spawnableObjet = Object.Instantiate(_spawnablePrefab);
                _poolService.Track(spawnableObjet);
            }
            
            spawnableObjet.gameObject.SetActive(true);
            
            return spawnableObjet;
        }

        private class ObjectPoolService
        {
            private readonly List<TSpawnableObjet> _pool = new List<TSpawnableObjet>();
            
            public bool HasFree => _pool.Count > 0;

            public TSpawnableObjet Get()
            {
                TSpawnableObjet spawnableObjet = _pool[0];
                _pool.Remove(spawnableObjet);
                spawnableObjet.Dissapear += Add;
                return spawnableObjet;
            }

            public void Track(TSpawnableObjet newObject)
            {
                newObject.Dissapear += Add;
            }

            private void Add(ISpawnable takenObject)
            {
                if (takenObject is TSpawnableObjet spawnableObjet)
                {
                    _pool.Add(spawnableObjet);
                    spawnableObjet.Dissapear -= Add;
                }
            }
        }
    }
}