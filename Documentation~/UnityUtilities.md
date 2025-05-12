## Animation Post Processing
- Create an AnimationPostProcessingSO scriptable object:
  - Menu item: `AnimationPostProcessor/Settings`
- The scriptable object just needs to exist within the project. DO NOT make more than one. Only the first one found will be used.
- AnimationsPostProcessingSO has the following properties:
  - bool Enabled
  - string TargetFolder
  - Avatar ReferenceAvatar
  - GameObject ReferenceFBX
  - bool EnableTranslationDoF
  - ModelImporterAnimationType AnimationType
  - bool LoopTime
  - bool RenameClips
  - bool ForceEditorApply
  - bool ExtractTextures
- Consider keeping `Enabled` set to false until you are ready to import your animations and set it to false afterwards as well to prevent unnecessary warnings and/or errors.
- Import your model and configure the settings as needed.
- Import an animation and configure the Settings SO as needed.
- Attach the example avatar and animation FBX to the Settings SO.
- Set `Enabled` to true.
- Import your animations in the `TargetFolder` specified in the Settings SO. Default: `Assets/_Project/Animations` (or a subfolder).
- They should now have the same settings applied to them as your example animation as well as the specified settings in the Settings SO.

---

## Editor Tools
- `Inspector Locker`: Locks the inspector with a keyboard shortcut and menu item.
  - Menu item: `Edit/Lock Inspector`
  - Keyboard shortcut: `Cmd + L`

---

## Timers
- `Timer`: An abstract timer class implemented.
  - void Tick(float deltaTime)
    - Call in Update method to progress timer.
  - void Reset()
    - Resets the timer to its initial state.
  - bool IsRunning()
  - bool GetTime()
  - bool Pause()
  - bool Resume()
  - Action OnTimerStart
    - Event that gets invoked when the timer starts.
  - Action OnTimerStop
    - Event that gets invoked when the timer stops.
  - float Progress
    - The current progress of the timer as a fraction.
  - Concrete classes:
    - `StopwatchTimer`: A timer that counts up.
      - override void Progress(float deltaTime)
        - Always returns -1f
    - `CountdownTimer`: A timer that counts down.
      - bool IsFinished
        - Returns true when the timer has reached zero.

---

## Singletons

- `Singleton\<T>:`: A regular ol' MonoBehaviour generic singleton.
- `PersistentSingleton\<T>:`: A generic singleton that persists between scenes.

---

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
    - float LogarithmicVolumeToSlider(this float decibels)

---

- **Tools**:
    - `SquashAndStretch`: A MonoBehaviour that allows for squash and stretch animations.

---

- **Data**:
    - `RuntimeScriptableObject`: An abstract class that ensure proper initialization of runtime Scriptable Objects.

