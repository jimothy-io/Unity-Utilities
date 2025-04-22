using UnityEngine;

namespace Jimothy.Utilities.Extensions
{
    public static class MonoBehaviourExtensions
    {
        public static void StopAndNullifyCoroutine(this MonoBehaviour monoBehaviour, ref Coroutine coroutine)
        {
            if (coroutine != null)
            {
                monoBehaviour.StopCoroutine(coroutine);
                coroutine = null;
            }
        }
    }
}