using UnityEngine;

namespace MainGame.Scripts.Infrastructure
{
    public class LoadLevelState : IPayloadState<SceneName>
    {
        private const string PlayerPath = "Prefabs/Player/Player";
        private const string Playerinitialpoint = "PlayerInitialPoint";
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
            GameObject initialPoint = GameObject.FindWithTag(Playerinitialpoint);
            
            GameObject playerPrefab = Instantiate(PlayerPath, at: initialPoint.transform.position);
        }

        private static GameObject Instantiate(string path)
        {
            GameObject playerPrefab = Resources.Load<GameObject>(path);
            
            return Object.Instantiate(playerPrefab); 
        }
        
        private static GameObject Instantiate(string path, Vector3 at)
        {
            GameObject playerPrefab = Resources.Load<GameObject>(path);
            
            return Object.Instantiate(playerPrefab, at, Quaternion.identity); 
        }

        public void Exit()
        {
        }
    }
}