using MainGame.Scripts.Infrastructure.AssetManagment;
using MainGame.Scripts.Infrastructure.Factory;
using MainGame.Scripts.Infrastructure.Services;
using MainGame.Scripts.Infrastructure.Services.InputService;
using MainGame.Scripts.Infrastructure.Services.PersistentProgress;
using MainGame.Scripts.UI;
using UnityEngine;

namespace MainGame.Scripts.Infrastructure.StateMachine.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices allServices)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _services = allServices;
            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(SceneName.Initial, onLoaded: EnterLoadedLevel);
        }

        public void Exit()
        { }

        private void EnterLoadedLevel()
        {
            _gameStateMachine.Enter<LoadProgressState>();
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<IInputService>(RegisterInputService());
            _services.RegisterSingle<IAsset>(new Asset());
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAsset>()));
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentProgressService>(), _services.Single<IGameFactory>()));
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
                Resources.Load<MobileJoystickHud>(AssetPath.MobileJoystickHud);
                
            mobileJoystickHud = Object.Instantiate(mobileJoystickHud);
                
            return new MobileInputService(mobileJoystickHud.FloatingJoystick);
        }
    }
}