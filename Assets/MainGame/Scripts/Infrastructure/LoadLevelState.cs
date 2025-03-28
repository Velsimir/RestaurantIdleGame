namespace MainGame.Scripts.Infrastructure
{
    public class LoadLevelState : IPayloadState<SceneName>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter(SceneName name)
        {
            _sceneLoader.Load(name);
        }

        public void Exit()
        {
        }
    }
}