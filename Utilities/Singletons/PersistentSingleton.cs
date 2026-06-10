using UnityEngine;

namespace Jimothy.Utilities.Singletons
{
    public abstract class PersistentSingleton<T> : MonoBehaviour where T : Component
    {
        [SerializeField] private bool _autoUnparentOnWake = true;

        private static T _instance;

        public static T Instance => _instance;

        protected virtual void Awake() => Init();

        protected virtual void Init()
        {
            if (!Application.isPlaying) return;

            if (_autoUnparentOnWake)
            {
                transform.SetParent(null);
            }

            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}