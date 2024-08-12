using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Jimothy.Utilities.Extensions
{
    public static class TransformExtensions
    {
        public static IEnumerable<Transform> Children(this Transform parent)
        {
            return parent.Cast<Transform>();
        }
        
        public static void Reset(this Transform transform)
        {
            transform.position = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }
        
        public static void DestroyChildren(this Transform parent)
        {
            parent.ForEveryChild(child => Object.DestroyImmediate(child.gameObject));
        }
        
        public static void ForEveryChild(this Transform parent, Action<Transform> action)
        {
            foreach (Transform child in parent)
            {
                action(child);
            }
        }
        
        public static void EnableChildren(this Transform parent, bool enable = true)
        {
            parent.ForEveryChild(child => child.gameObject.SetActive(enable));
        }
        
        public static void SetX(this Transform transform, float x)
        {
            transform.position = transform.position.With(x: x);
        }
        
        public static void SetY(this Transform transform, float y)
        {
            transform.position = transform.position.With(y: y);
        }
        
        public static void SetZ(this Transform transform, float z)
        {
            transform.position = transform.position.With(z: z);
        }
    }
}