using UnityEngine;

namespace Jimothy.Utilities.Singletons
{
    public class PersistentSingleton<T> : MonoBehaviour where T : Component
    {
        [SerializeField] private bool _autoUnparentOnWake = true;
        
        protected static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;
                
                _instance = FindAnyObjectByType<T>();
                if (_instance == null)
                {
                    Debug.LogWarning($"No instance of {typeof(T)} found in scene! Creating one...");
                    var newObject = new GameObject(typeof(T).Name + "(auto-generated)");
                    _instance = newObject.AddComponent<T>();
                }

                return _instance;
            }
        }

        protected virtual void Awake()
        {
            InitializeSingleton();
        }

        protected virtual void InitializeSingleton()
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
            else
            {
                if (_instance != this)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}