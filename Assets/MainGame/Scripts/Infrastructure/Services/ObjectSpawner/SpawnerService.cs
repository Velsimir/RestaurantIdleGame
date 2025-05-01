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
        
        public TSpawnableObjet Spawn(Transform at)
        {
            TSpawnableObjet spawnableObjet;

            if (_poolService.HasFree)
            {
                spawnableObjet = _poolService.Get();
            }
            else
            {
                spawnableObjet = Object.Instantiate(_spawnablePrefab, at);
                _poolService.Track(spawnableObjet);
            }
            
            spawnableObjet.transform.position = at.position;
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
                spawnableObjet.Disappeared += Add;
                return spawnableObjet;
            }

            public void Track(TSpawnableObjet newObject)
            {
                newObject.Disappeared += Add;
            }

            private void Add(ISpawnable takenObject)
            {
                if (takenObject is TSpawnableObjet spawnableObjet)
                {
                    _pool.Add(spawnableObjet);
                    spawnableObjet.Disappeared -= Add;
                }
            }
        }
    }
}