using System.Collections;
using MainGame.Scripts.Logic.PlayerLogic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MainGame.Scripts.Logic
{
    public class ObjectActivator : MonoBehaviour
    {
        [InterfaceField(typeof(IActivatable))]
        [SerializeField] private Object _activatedObject;
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private float _timeToActivateCoinSpender;
        [SerializeField] private ObjectHoldPoint _coinSpawnPoint;
        [SerializeField] private int _coinsToActivateObject;
        [SerializeField] private float _timeBetweenTakeCoins;
        
        private WaitForSeconds _waitToActivate;
        private Coroutine _coroutineCounterPlayerInside;
        private CoinSpender _coinSpender;
        
        private IActivatable ActivatableObject => _activatedObject as IActivatable;

        private void Awake()
        {
            _waitToActivate = new WaitForSeconds(_timeToActivateCoinSpender);
            _coinSpender = new CoinSpender(_coinSpawnPoint, _coinsToActivateObject,_timeBetweenTakeCoins);
        }

        private void OnEnable()
        {
            _triggerObserver.CollusionEntered += StartCountToActivate;
            _triggerObserver.CollusionExited += Deactivate;
            _coinSpender.AllCoinsPayed += Activate;
        }

        private void OnDisable()
        {
            _triggerObserver.CollusionEntered -= StartCountToActivate;
            _triggerObserver.CollusionExited -= Deactivate;
            _coinSpender.AllCoinsPayed -= Activate;
        }

        private void Deactivate(Collider obj)
        {
            if (obj.TryGetComponent(out Player player))
            {
                StopCurrentCoroutine(ref _coroutineCounterPlayerInside);
                _coinSpender.StopSpawn();
            }
        }

        private void StartCountToActivate(Collider obj)
        {
            if (obj.TryGetComponent(out Player player))
            {
                StopCurrentCoroutine(ref _coroutineCounterPlayerInside);
                
                _coroutineCounterPlayerInside = StartCoroutine(CountTimePlayerInside(player.GetComponent<PlayerWallet>()));
            }
        }

        private IEnumerator CountTimePlayerInside(PlayerWallet playerWallet)
        {
            yield return _waitToActivate;
            _coinSpender.StartSpawn(playerWallet);
        }

        private void Activate()
        {
            ActivatableObject.Activate();
            gameObject.SetActive(false);
        }

        private void StopCurrentCoroutine(ref Coroutine coroutine)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }
        }
    }
}