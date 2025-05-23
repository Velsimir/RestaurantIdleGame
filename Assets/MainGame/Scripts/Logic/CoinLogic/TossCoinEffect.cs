using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MainGame.Scripts.Logic.CoinLogic
{
    [RequireComponent(typeof(Rigidbody),
        typeof(Collider))]
    public class TossCoinEffect : MonoBehaviour
    {
        [Header("Настройки")] 
        [SerializeField] private float _tossForce = 2f;
        [SerializeField] private float _rotationSpeed = 360f;
        [SerializeField] private float _flightDuration = 2f;
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private Collider _collider;

        private bool _isFlying;
        private Coroutine _flightRoutine;
        
        public event Action EffectEnded;

        public void Toss()
        {
            if (_isFlying) return;

            _isFlying = true;

            TurnOffPhysic();

            if (_flightRoutine != null)
            {
                StopCoroutine(_flightRoutine);
                _flightRoutine = null;
            }
            
            _flightRoutine = StartCoroutine(FlyRoutine());
        }

        private void TurnOffPhysic()
        {
            _rb.isKinematic = true;
            _collider.enabled = false;
        }

        private IEnumerator FlyRoutine()
        {
            float elapsed = 0f;
            Vector3 startPos = transform.position;
            Vector3 randomDirection = new Vector3(
                Random.Range(-0.3f, 0.3f),
                Random.Range(0.8f, 1.2f),
                Random.Range(-0.3f, 0.3f)
            ).normalized;

            while (elapsed < _flightDuration)
            {
                float progress = elapsed / _flightDuration;

                float height = Mathf.Sin(progress * Mathf.PI);

                transform.position = startPos +
                                     randomDirection * _tossForce * progress +
                                     Vector3.up * height * 2f;

                transform.Rotate(Vector3.right, _rotationSpeed * Time.deltaTime);

                elapsed += Time.deltaTime;
                yield return null;
            }

            LandCoin();
        }

        private void LandCoin()
        {
            _isFlying = false;

            _rb.isKinematic = false;
            _collider.enabled = true;
            
            EffectEnded?.Invoke();
        }
    }
}