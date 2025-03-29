using MainGame.Scripts.Services.InputService;
using MainGame.Scripts.UI;
using UnityEngine;

namespace MainGame.Scripts.Infrastructure
{
    public class BootstrapState : IState
    {
        private const string MobileJoystickHudPath = "Prefabs/UI/MobileJoystick/MobileJoystickHud";
        
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            RegisterServices();
            _sceneLoader.Load(SceneName.Initial, onLoaded: EnterLoadedLevel);
        }

        private void EnterLoadedLevel()
        {
            _gameStateMachine.Enter<LoadLevelState, SceneName>(SceneName.Game);
        }

        public void Exit()
        { }

        private void RegisterServices()
        {
            Game.InputService = RegisterInputService();
        }
        
        private IInputService RegisterInputService()
        {
            if (Application.isEditor == false)
            {
                return new StandaloneInputService();
            }
            else
            {
                return CreateMobileInput();
            }
        }

        private static IInputService CreateMobileInput()
        {
            MobileJoystickHud mobileJoystickHud = 
                Resources.Load<MobileJoystickHud>(MobileJoystickHudPath);
                
            mobileJoystickHud = Object.Instantiate(mobileJoystickHud);
                
            return new MobileInputService(mobileJoystickHud.FloatingJoystick);
        }
    }
}