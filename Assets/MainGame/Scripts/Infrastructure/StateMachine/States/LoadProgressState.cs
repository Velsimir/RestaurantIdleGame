using MainGame.Scripts.Data;
using MainGame.Scripts.Infrastructure.Services.PersistentProgress;
using MainGame.Scripts.Infrastructure.Services.SaveLoad;
using UnityEngine;
using YG;

namespace MainGame.Scripts.Infrastructure.StateMachine.States
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
            _gameStateMachine.Enter<LoadLevelState, SceneName>(_progressService.Progress.WorldData.PositionOnLevel.Level);
        }

        public void Exit()
        {
        }

        private void LoadProgressionOrInitNew()
        {
            PlayerProgress loadedProgress = _saveLoadService.LoadProgress();

            if (loadedProgress != null)
            {
                _progressService.Progress = loadedProgress;
            }
            else
            {
                _progressService.Progress = LoadNewProgress();
            }
        }

        private PlayerProgress LoadNewProgress()
        {
            YG2.saves.PlayerProgress = new PlayerProgress(initialLevel: SceneName.Game);
            YG2.SaveProgress();
            return YG2.saves.PlayerProgress;
        }
    }
}