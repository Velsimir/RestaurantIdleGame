using MainGame.Scripts.Infrastructure.Factory;
using MainGame.Scripts.Infrastructure.Services.PersistentProgress;
using MainGame.Scripts.Logic.PlayerLogic;
using MainGame.Scripts.UI;
using UnityEngine;

namespace MainGame.Scripts.Infrastructure.StateMachine.States
{
    public class LoadLevelState : IPayloadState<SceneName>
    {
        private const string PlayerInitialPoint = "PlayerInitialPoint";
        
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly Curtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, Curtain curtain, IGameFactory gameFactory, IPersistentProgressService progressService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        public void Enter(SceneName name)
        {
            _curtain.Show();
            _gameFactory.Cleanup();
            _sceneLoader.Load(name, OnLoaded);
        }

        public void Exit()
        {
            _curtain.Hide();
        }

        private void OnLoaded()
        {
            GameObject initialPoint = GameObject.FindWithTag(PlayerInitialPoint);
            GameObject player = _gameFactory.CreateHero(initialPoint.transform);
            _gameFactory.CreateHUD(player.GetComponent<PlayerWallet>());
            InformProgressReaders();
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
            {
                progressReader.LoadProgress(_progressService.Progress);
            }
        }
    }
}