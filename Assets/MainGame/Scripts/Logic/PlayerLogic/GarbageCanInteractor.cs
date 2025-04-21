using System.Collections;
using MainGame.Scripts.Logic.Tables;
using UnityEngine;

namespace MainGame.Scripts.Logic.PlayerLogic
{
    public class GarbageCanInteractor : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private PlayerPizzaTaker _playerPizzaTaker;
        [SerializeField] private float _delay;

        private WaitForSeconds _wait;
        private Coroutine _takeOutTrash;
        private bool _isWorking = true;

        private void Awake()
        {
            _wait = new WaitForSeconds(_delay);
        }

        private void OnEnable()
        {
            _triggerObserver.CollusionEntered += TakeOutTrash;
            _triggerObserver.CollusionExited += StopOutTrash;
        }

        private void OnDisable()
        {
            _triggerObserver.CollusionEntered -= TakeOutTrash;
            _triggerObserver.CollusionExited -= StopOutTrash;
        }

        private void TakeOutTrash(Collider collider)
        {
            if (collider.transform.TryGetComponent(out GarbageCan garbageCan))
            {
                _isWorking = true;
                
                StopCurrentCoroutine();
                
                _takeOutTrash = StartCoroutine(DoTheGarbage(garbageCan));
            }
        }

        private IEnumerator DoTheGarbage(GarbageCan garbageCan)
        {
            while (_isWorking)
            {
                if (_playerPizzaTaker.HasPizza)
                {
                    garbageCan.TakeOutGarbage(_playerPizzaTaker.GetPizza());
                }

                yield return _wait;
            }
        }

        private void StopOutTrash(Collider collider)
        {
            _isWorking = false;
        }
        
        private void StopCurrentCoroutine()
        {
            if (_takeOutTrash != null)
            {
                StopCoroutine(_takeOutTrash);
                _takeOutTrash = null;
            }
        }
    }
}