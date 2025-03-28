using System.Collections;
using UnityEngine;

namespace MainGame.Scripts.Infrastructure
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}