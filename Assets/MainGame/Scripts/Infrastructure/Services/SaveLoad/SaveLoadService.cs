using MainGame.Scripts.Data;
using MainGame.Scripts.ExtensionMethods;
using MainGame.Scripts.Infrastructure.Factory;
using MainGame.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace MainGame.Scripts.Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";

        private readonly IPersistentProgressService _progressService;
        private readonly IGameFactory _gameFactory;

        public SaveLoadService(IPersistentProgressService progressService,IGameFactory gameFactory)
        {
            _progressService = progressService;
            _gameFactory = gameFactory;
        }

        public void Save()
        {
            foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters)
            {
                progressWriter.UpdateProgress(_progressService.Progress);
            }
            
            PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
        }

        public PlayerProgress Load()
        {
            string progress = PlayerPrefs.GetString(ProgressKey);

            if (progress != null)
            {
                return progress.ToDeserialized<PlayerProgress>();
            }
            else
            {
                return null;
            }
        }
    }
}