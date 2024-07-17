## Singletons

- **Singleton\<T>:**: A regular ol' MonoBehaviour generic singleton.
- **PersistentSingleton\<T>:**: A generic singleton that persists between scenes.

## Extension Methods

- **Transform Extensions**:

    - IEnumerable\<Transform> Children(this Transform parent)
    - void Reset(this Transform transform)
    - void DestroyChildren(this Transform parent)
    - void ForEveryChild(this Transform parent, Action\<Transform> action)
    - void EnableChildren(this Transform parent, bool enable = true)
    - void SetX(this Transform transform, float x)
    - void SetY(this Transform transform, float y)
    - void SetZ(this Transform transform, float z)

- **GameObject Extensions**:

    - T OrNull\<T>(this T gameObject) where T : Object
    - T GetOrAdd\<T>(this GameObject gameObject) where T : Component
    - void DestroyChildren(this GameObject gameObject)
    - void EnableChildren(this GameObject gameObject, bool enable = true)
    - void ResetTransform(this GameObject gameObject)

- **Enumerable Extensions**:

    - void ForEach<T>(this IEnumerable<T> enumerables, Action<T> action)

- **Vector2 Extensions**:

    - Vector2 Add(this Vector2 vector, float x = 0, float y = 0)
    - Vector2 With(this Vector2 vector, float? x = null, float? y = null)
    - bool InRangeOf(this Vector2 current, Vector2 target, float range)
    - Vector3 ToVector3(this Vector2 vector, float y = 0)

- **Vector3 Extensions**:

    - Vector3 Add(this Vector3 vector, float x = 0, float y = 0, float z = 0)
    - Vector3 With(this Vector3 vector, float? x = null, float? y = null, float? z = null)

- **Audio Extensions**:

    - float ToLogarithmicVolume(this float sliderValue)
    - float ToLogarithmicFraction(this float fraction)