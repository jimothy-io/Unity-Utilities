using UnityEngine;

namespace Jimothy.Utilities.Singletons
{
    public abstract class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;

        public static T Instance => _instance;

        protected virtual void Awake() => Init();

        protected virtual void Init()
        {
            if (!Application.isPlaying) return;

            if (_instance == null)
            {
                _instance = this as T;
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}