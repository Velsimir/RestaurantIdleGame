using UnityEngine;

namespace MainGame.Scripts.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private GameBootstrapper _gameBootstrapperPrefab;

        private void Awake()
        {
            GameBootstrapper gameBootstrapper = FindObjectOfType<GameBootstrapper>();

            if (gameBootstrapper == null)
            {
                Instantiate(_gameBootstrapperPrefab);
            }
        }
    }
}