using System.Collections.Generic;
using UnityEngine;

namespace Jimothy.Utilities.Data
{
    public abstract class RuntimeScriptableObject : ScriptableObject
    {
        private static readonly List<RuntimeScriptableObject> Instances = new();

        protected abstract void Reset();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void ResetAllInstances()
        {
            foreach (var instance in Instances)
            {
                instance.Reset();
            }
        }

        private void OnEnable()
        {
            Instances.Add(this);
        }

        private void OnDisable()
        {
            Instances.Remove(this);
        }
    }
}