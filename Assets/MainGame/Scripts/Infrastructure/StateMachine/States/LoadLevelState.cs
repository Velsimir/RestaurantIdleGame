using MainGame.Scripts.Infrastructure.Factory;
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

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, Curtain curtain, IGameFactory gameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
        }

        public void Enter(SceneName name)
        {
            _curtain.Show();
            _sceneLoader.Load(name, OnLoaded);
        }

        public void Exit()
        {
            _curtain.Hide();
        }

        private void OnLoaded()
        {
            GameObject initialPoint = GameObject.FindWithTag(PlayerInitialPoint);
            
            GameObject playerPrefab = _gameFactory.CreateHero(initialPoint);
            
            _gameStateMachine.Enter<GameLoopState>();
        }
    }
}