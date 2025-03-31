using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainGame.Scripts.Infrastructure
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;
        
        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public SceneName CurrentScene { get; private set; }

        public void Load(SceneName name, Action onLoaded = null)
        {
            _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));
            CurrentScene = name;
        }

        private IEnumerator LoadScene(SceneName name, Action onLoaded = null)
        {
            string sceneName = name.ToString();
            
            if (SceneManager.GetActiveScene().name == sceneName)
            {
                onLoaded?.Invoke();
                yield break;
            }
            
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(sceneName);

            while (waitNextScene?.isDone == false)
            {
                yield return null;
            }
            
            onLoaded?.Invoke();
        }
    }
}