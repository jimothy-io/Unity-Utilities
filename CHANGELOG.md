## [1.4.1] - 2024-11-03
### Fix build errors
- Creates Editor assembly definitions for all editor scripts to prevent build errors.

## [1.4.0] - 2024-08-18
### Adds animation post processing
- Adds a ScriptableObject `AnimationPostProcessingSO` that allows for batch processing of imported animations.

## [1.3.0] - 2024-08-15
### Adds inspector lock keyboard shortcut
- Adds a keyboard shortcut and menu item for the "inspector lock" feature.

## [1.2.0] - 2024-08-13
### Adds timer classes
- Adds an abstract `Timer` class and the following concrete classes:
  - `StopwatchTimer`
  - `CountdownTimer`

## [1.1.0] - 2024-07-17
### Adds audio related extension methods
- Adds extension methods for following classes:
  - `float`

## [1.0.0] - 2024-06-24
### First Release
- Adds extension methods for following classes:
  - `IEnumerable`
  - `Transform`
  - `GameObject`
  - `Vector2`
  - `Vector3`
- Adds singleton implementations:
  - MonoBehaviour
  - Persistent MonoBehaviour
