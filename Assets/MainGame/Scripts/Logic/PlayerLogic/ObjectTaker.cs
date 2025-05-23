using System.Collections;
using System.Collections.Generic;
using MainGame.Scripts.Infrastructure.Services;
using MainGame.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace MainGame.Scripts.Logic.PlayerLogic
{
    public class ObjectTaker : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private ObjectHoldPoint _holdObjectPoint;
        [SerializeField] private float _delay = 0.2f;

        private WaitForSeconds _waitDelay;
        private Stack<ITakable> _tackables;
        private int _maxObjects;
        private Coroutine _takingCoroutine;
        private ObjectStacker _objectStacker;

        public bool HasObjects => _tackables.Count > 0;

        private void Awake()
        {
            _tackables = new Stack<ITakable>();
            _maxObjects = AllServices.Container.Single<IPersistentProgressService>().Progress.MaxPizzaHoldCount;
            _waitDelay = new WaitForSeconds(_delay);
            _objectStacker = new ObjectStacker();
        }

        private void OnEnable()
        {
            _triggerObserver.CollusionEntered += TryTakeObject;
        }

        private void OnDisable()
        {
            _triggerObserver.CollusionEntered -= TryTakeObject;
        }

        public ITakable GetObject()
        {
            return _tackables.Pop();
        }

        private void TryTakeObject(Collider collider)
        {
            if (collider.TryGetComponent(out IGiveble giveble))
            {
                if (_takingCoroutine != null)
                {
                    StopCoroutine(_takingCoroutine);
                    _takingCoroutine = null;
                }

                _takingCoroutine = StartCoroutine(TakingObjects(giveble));
            }
        }

        private IEnumerator TakingObjects(IGiveble giveble)
        {
            while (giveble.HasObjects && _tackables.Count < _maxObjects)
            {
                ITakable takable = giveble.GetObject();
                
                takable.Take(_holdObjectPoint.Transform,
                    _objectStacker.GetSpawnPoint(_tackables, _holdObjectPoint.Transform));
                _tackables.Push(takable);

                yield return _waitDelay;
            }
        }
    }
}