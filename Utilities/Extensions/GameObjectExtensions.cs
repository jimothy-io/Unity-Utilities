using UnityEngine;

namespace Jimothy.Utilities.Extensions
{
    public static class GameObjectExtensions
    {
        public static T OrNull<T>(this T gameObject) where T : Object =>
            gameObject ? gameObject : null;

        public static T GetOrAdd<T>(this GameObject gameObject) where T : Component
        {
            T component = gameObject.GetComponent<T>();
            if (!component)
            {
                component = gameObject.AddComponent<T>();
            }

            return component;
        }
        
        public static void DestroyChildren(this GameObject gameObject)
        {
            gameObject.transform.DestroyChildren();
        }
        
        public static void EnableChildren(this GameObject gameObject, bool enable = true)
        {
            gameObject.transform.EnableChildren(enable);
        }

        public static void ResetTransform(this GameObject gameObject)
        {
            gameObject.transform.Reset();
        }
    }
}