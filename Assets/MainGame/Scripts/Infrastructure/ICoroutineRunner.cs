using System.Collections;
using MainGame.Scripts.Infrastructure.Services;
using UnityEngine;

namespace MainGame.Scripts.Infrastructure
{
    public interface ICoroutineRunner : IService
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
        void StopCoroutine(ref Coroutine coroutine);
    }
}