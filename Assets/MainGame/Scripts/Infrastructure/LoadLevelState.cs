using MainGame.Scripts.UI;
using UnityEngine;

namespace MainGame.Scripts.Infrastructure
{
    public class LoadLevelState : IPayloadState<SceneName>
    {
        private const string PlayerPath = "Prefabs/Player/Player";
        private const string PlayerInitialPoint = "PlayerInitialPoint";
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly Curtain _curtain;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, Curtain curtain)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
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
            
            GameObject playerPrefab = Instantiate(PlayerPath, at: initialPoint.transform.position);
            
            _gameStateMachine.Enter<GameLoopState>();
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
    }
}