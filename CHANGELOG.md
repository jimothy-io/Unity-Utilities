## [Unreleased]

---

## [1.10.2] - 2025-06-07
### Cleans up context menu items
- Organize context menu items into submenus.

---

## [1.10.1] - 2025-06-05
### Fixes minor bugs
- Uncaps animation preview length.

---

## [1.10.0] - 2025-05-22
### Adds new attribute
- Adds `[Required]` field attribute to mark fields as required in the inspector.

---

## [1.9.1] - 2025-05-20
### Fixes minor bugs
- Fixes namespace conflicts.

## [1.9.0] - 2025-05-20
### Adds animator state preview
- Adds animation preview to animation controller inspector.

## [1.8.0] - 2025-05-12
### Renames package and updates documentation
- Renames this package from `Unity Utility Library` to `jUnityUtilities`.

---

## [1.7.0] - 2025-05-01
### Creates Data namespace and adds abstract Scriptable Object class
- Adds `RuntimeScriptableObject` to `Data`.

---

## [1.6.0] - 2025-04-22
### Creates Tools namespace and adds squash and stretch MonoBehaviour
- Adds `SquashAndStretch` MonoBehaviour component to `Tools`.

---

## [1.5.0] - 2025-04-22
### Adds coroutine MonoBehaviour extension method
- Adds `StopAndNullifyCoroutine` extension method to MonoBehaviour.

---

## [1.4.2] - 2025-03-02
### Adds audio extension method
- Adds inverse of existing `SliderToLogarithmicVolume` extension method; `LogarithmicVolumeToSlider` to `Extensions`.

---

## [1.4.1] - 2024-11-03
### Fixes build errors
- Creates editor assembly definitions for all editor scripts to prevent build errors.

---

## [1.4.0] - 2024-08-18
### Adds animation post-processing
- Adds a scriptable object `AnimationPostProcessingSO` that allows for batch processing of imported animations.

---

## [1.3.0] - 2024-08-15
### Adds inspector lock keyboard shortcut
- Adds a keyboard shortcut and menu item for the "inspector lock" feature.

---

## [1.2.0] - 2024-08-13
### Adds timer classes
- Adds an abstract `Timer` class and the following concrete classes:
  - `StopwatchTimer`
  - `CountdownTimer`

---

## [1.1.0] - 2024-07-17
### Adds audio related extension methods
- Adds extension methods for following classes:
  - `float`

---

## [1.0.0] - 2024-06-24
### First Release
- Adds extension methods for following classes:
  - `IEnumerable`
  - `Transform`
  - `GameObject`
  - `Vector2`
  - `Vector3`
- Adds singleton implementations:
  - `MonoBehaviour`
  - Persistent `MonoBehaviour`
