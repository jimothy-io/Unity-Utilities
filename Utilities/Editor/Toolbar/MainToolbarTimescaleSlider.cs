using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.UIElements;

namespace Jimothy.Utilities.Editor.Toolbar
{
    public class MainToolbarTimescaleSlider
    {
        private const float MinTimeScale = 0f;
        private const float MaxTimeScale = 5f;
        private const float Padding = 10f;

        [MainToolbarElement("Timescale/Slider", defaultDockPosition = MainToolbarDockPosition.Middle)]
        public static MainToolbarElement TimeSlider()
        {
            var content = new MainToolbarContent("Timescale", "Timescale");
            var slider =
                new MainToolbarSlider(content, Time.timeScale, MinTimeScale, MaxTimeScale, OnSliderValueChanged);

            slider.populateContextMenu = (menu) =>
            {
                menu.AppendAction("Reset", _ =>
                {
                    Time.timeScale = 1f;
                    MainToolbar.Refresh("Timescale/Slider");
                });
            };

            MainToolbarElementStyler.StyleElement<VisualElement>("Timescale/Slider", element =>
            {
                element.style.paddingLeft = Padding;
            });

            return slider;
        }

        static void OnSliderValueChanged(float value)
        {
            Time.timeScale = value;
        }
    }
}