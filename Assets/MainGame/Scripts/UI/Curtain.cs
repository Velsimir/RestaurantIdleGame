using System.Collections;
using UnityEngine;

namespace MainGame.Scripts.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Curtain : MonoBehaviour
    {
        [SerializeField] private float _alphaFadeInStep = 0.1f;
        [SerializeField] private float _timeFadeInStep = 0.01f;
        
        private CanvasGroup _canvasGroup;
        private WaitForSeconds _wait;
        
        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _wait = new WaitForSeconds(_timeFadeInStep);
            DontDestroyOnLoad(this);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _canvasGroup.alpha = 1;
        }

        public void Hide()
        {
            StartCoroutine(FadeIn());
        }

        private IEnumerator FadeIn()
        {
            while (_canvasGroup.alpha > 0)
            {
                _canvasGroup.alpha -= _alphaFadeInStep;

                yield return _wait;
            }
            
            gameObject.SetActive(false);
        }
    }
}