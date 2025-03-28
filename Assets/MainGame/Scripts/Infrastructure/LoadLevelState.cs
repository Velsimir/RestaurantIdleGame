using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainGame.Scripts.Infrastructure
{
    public class LoadLevelState : IPayloadState<SceneName>
    {
        private const string PlayerPlayer = "Player/Player";
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter(SceneName name)
        {
            _sceneLoader.Load(name, OnLoaded);
        }

        private void OnLoaded()
        {
            GameObject playerPrefab = PlayerPrefab();
        }

        private static GameObject PlayerPrefab()
        {
            GameObject playerPrefab = Resources.Load<GameObject>(PlayerPlayer);
            
            return Object.Instantiate(playerPrefab); 
        }

        public void Exit()
        {
        }
    }
}