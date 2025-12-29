using UnityEngine;
using UnityEditor;
using UnityEditor.Toolbars;

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
    }
}