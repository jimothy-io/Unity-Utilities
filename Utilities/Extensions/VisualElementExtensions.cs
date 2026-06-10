using System;
using UnityEngine.UIElements;

namespace Jimothy.Utilities.Extensions
{
    public static class VisualElementExtensions
    {
        private static VisualElement FindElement(this VisualElement element, Func<VisualElement, bool> predicate)
        {
            return predicate(element) ? element : element.Query<VisualElement>().Where(predicate).First();
        }

        public static VisualElement FindElementByName(this VisualElement element, string name)
        {
            return element.FindElement(e => e.name == name);
        }

        public static VisualElement FindElementByTooltip(this VisualElement element, string tooltip)
        {
            return element.FindElement(e => e.tooltip == tooltip);
        }
    }
}