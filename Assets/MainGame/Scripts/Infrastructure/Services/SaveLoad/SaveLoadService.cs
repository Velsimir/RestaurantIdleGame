using MainGame.Scripts.Data;
using MainGame.Scripts.Infrastructure.Factory;
using MainGame.Scripts.Infrastructure.Services.PersistentProgress;
using YG;

namespace MainGame.Scripts.Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly IPersistentProgressService _progressService;
        private readonly IGameFactory _gameFactory;

        public SaveLoadService(IPersistentProgressService progressService,IGameFactory gameFactory)
        {
            _progressService = progressService;
            _gameFactory = gameFactory;
        }

        public void SaveProgress()
        {
            foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters)
            {
                progressWriter.UpdateProgress(_progressService.Progress);
            }
            
            YG2.SaveProgress();
        }

        public PlayerProgress LoadProgress()
        {
            PlayerProgress progress = YG2.saves.PlayerProgress;
        
            if (progress != null)
            {
                return progress;
            }
            else
            {
                return null;
            }
        }
    }
}