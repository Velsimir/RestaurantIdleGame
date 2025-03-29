using MainGame.Scripts.Infrastructure.AssetManagment;
using MainGame.Scripts.Infrastructure.Factory;
using MainGame.Scripts.Infrastructure.Services;
using MainGame.Scripts.Infrastructure.Services.InputService;
using MainGame.Scripts.UI;
using UnityEngine;

namespace MainGame.Scripts.Infrastructure.StateMachine.States
{
    public class BootstrapState : IState
    {
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

        public void Exit()
        { }

        private void EnterLoadedLevel()
        {
            _gameStateMachine.Enter<LoadLevelState, SceneName>(SceneName.Game);
        }

        private void RegisterServices()
        {
            AllServices.Container.RegisterSingle<IInputService>(RegisterInputService());
            AllServices.Container.RegisterSingle<IGameFactory>(new GameFactory(AllServices.Container.Single<IAsset>()));
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
                Resources.Load<MobileJoystickHud>(AssetPath.MobileJoystickHudPath);
                
            mobileJoystickHud = Object.Instantiate(mobileJoystickHud);
                
            return new MobileInputService(mobileJoystickHud.FloatingJoystick);
        }
    }
}