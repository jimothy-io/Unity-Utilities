using UnityEngine;

namespace Jimothy.Utilities.Extensions
{
    public static class AudioExtensions
    {
        /// <summary>
        /// Converts a slider value to a logarithmic volume value.
        /// Ensures value is equal to or greater than 0.0001 to avoid log(0) errors.
        /// Takes base-10 logarithm of the slider value and multiplies the result with 20.
        /// Look, I'm going to be completely honest with you, I don't fully understand why we
        /// multiply by 20, but apparently it's convention when working with decibels for some
        /// reason so let's just go along with it, okay?
        /// </summary>
        public static float SliderToLogarithmicVolume(this float sliderValue)
        {
            return Mathf.Log10(Mathf.Max(sliderValue, 0.0001f)) * 20;
        }

        /// <summary>
        /// Converts a fraction in range [0, 1] to a logarithmic scale in range [0, 1].
        /// Useful for improved fading effects between AudioClips.
        /// </summary>
        public static float LinearFractionToLogarithmicFraction(this float fraction)
        {
            return Mathf.Log(1 + 9 * fraction) / Mathf.Log10(10);
        }
        
    }
}
