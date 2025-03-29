using MainGame.Scripts.UI;
using UnityEngine;

namespace MainGame.Scripts.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private Curtain _curtain;
        
        private Game _game;

        private void Awake()
        {
            _game = new Game(this, _curtain);
            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}
