using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.Toolbars;
using UnityEngine.UIElements;
using Jimothy.Utilities.Extensions;

namespace Jimothy.Utilities.Editor.Toolbar
{
    public class MainToolbarButtons : MonoBehaviour
    {
        [MainToolbarElement("Project/Open Project Settings", defaultDockPosition = MainToolbarDockPosition.Middle)]
        public static MainToolbarElement ProjectSettingsButton()
        {
            var icon = EditorGUIUtility.IconContent("SettingsIcon").image as Texture2D;
            var content = new MainToolbarContent(icon);

            return new MainToolbarButton(content, () => { SettingsService.OpenProjectSettings(); });
        }

        [MainToolbarElement("Timescale/Reset", defaultDockPosition = MainToolbarDockPosition.Middle)]
        public static MainToolbarElement ResetTimeScaleButton()
        {
            var icon = EditorGUIUtility.IconContent("Refresh").image as Texture2D;
            var content = new MainToolbarContent(icon, "Reset");
            var button = new MainToolbarButton(content, () =>
            {
                Time.timeScale = 1f;
                MainToolbar.Refresh("Timescale/Slider");
            });

            MainToolbarElementStyler.StyleElement<EditorToolbarButton>("Timescale/Reset", element =>
            {
                element.style.paddingLeft = 0f;
                element.style.paddingRight = 0f;
                element.style.marginLeft = 0f;
                element.style.marginRight = 0f;
                element.style.minWidth = 20f;
                element.style.maxWidth = 20f;

                var image = element.Q<Image>();
                if (image != null)
                {
                    image.style.width = 12f;
                    image.style.height = 12f;
                }
            });
            
            return button;
        }
    }

    public static class MainToolbarElementStyler
    {
        public static void StyleElement<T>(string elementName, Action<T> styleAction) where T : VisualElement
        {
            EditorApplication.delayCall += () =>
            {
                ApplyStyle(elementName, (element) =>
                {
                    T targetElement;

                    if (element is T typedElement)
                    {
                        targetElement = typedElement;
                    }
                    else
                    {
                        targetElement = element.Query<T>().First();
                    }

                    if (targetElement != null)
                    {
                        styleAction(targetElement);
                    }
                });
            };
        }

        private static void ApplyStyle(string elementName, Action<VisualElement> styleCallback)
        {
            var element = FindElementByName(elementName);
            if (element == null) return;
            
            styleCallback(element);
        }

        private static VisualElement FindElementByName(string name)
        {
            var windows = Resources.FindObjectsOfTypeAll<EditorWindow>();
            foreach (var window in windows)
            {
                var root = window.rootVisualElement;
                if (root == null) continue;

                VisualElement element;
                if ((element = root.FindElementByName(name)) != null) return element;
                if ((element = root.FindElementByTooltip(name)) != null) return element;
            }

            return null;
        }
    }
    
    
    

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
                element.style.paddingLeft = 10f;
            });

            return slider;
        }

        static void OnSliderValueChanged(float value)
        {
            Time.timeScale = value;
        }
    }
}