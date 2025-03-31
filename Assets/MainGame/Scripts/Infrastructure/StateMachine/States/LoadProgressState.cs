using MainGame.Scripts.Data;
using MainGame.Scripts.Infrastructure.Services.PersistentProgress;
using MainGame.Scripts.Infrastructure.StateMachine.States;

namespace MainGame.Scripts.Infrastructure.StateMachine
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressionOrInitNew();
            _gameStateMachine.Enter<LoadLevelState, SceneName>(_progressService.PlayerProgress.WorldData.PositionOnLevel.Level);
        }

        public void Exit()
        {
        }

        private void LoadProgressionOrInitNew()
        {
            var loadedProgress = _saveLoadService.Load();

            if (loadedProgress != null)
            {
                _progressService.PlayerProgress = loadedProgress;
            }
            else
            {
                _progressService.PlayerProgress = LoadNewProgress();
            }
        }

        private PlayerProgress LoadNewProgress()
        {
            return new PlayerProgress(initialLevel: SceneName.Initial);
        }
    }
}