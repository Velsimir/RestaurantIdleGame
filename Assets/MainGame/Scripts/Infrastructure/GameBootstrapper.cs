using MainGame.Scripts.Infrastructure.StateMachine.States;
using MainGame.Scripts.UI;
using UnityEngine;

namespace MainGame.Scripts.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private Curtain _curtainPrefab;
        
        private Game _game;

        private void Awake()
        {
            _curtainPrefab = Instantiate(_curtainPrefab);
            
            _game = new Game(coroutineRunner: this, _curtainPrefab);
            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}
