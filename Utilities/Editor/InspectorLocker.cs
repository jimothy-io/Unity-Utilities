using System.Reflection;
using UnityEngine;
using UnityEditor;

namespace Jimothy.Utilities.Editor
{
    public static class InspectorLocker
    {
        private static readonly MethodInfo FlipLocked;
        private const BindingFlags Flags = BindingFlags.NonPublic | BindingFlags.Instance;

        static InspectorLocker()
        {
            // Cache static MethodInfo and PropertyInfo for performance
            var editorLockTrackerType =
                typeof(EditorGUIUtility).Assembly.GetType(
                    "UnityEditor.EditorGUIUtility+EditorLockTracker");
            FlipLocked = editorLockTrackerType.GetMethod("FlipLocked", Flags);
        }
        
        [MenuItem("Edit/Lock Inspector %l")]
        public static void ToggleLock()
        {
            ActiveEditorTracker.sharedTracker.isLocked =
                !ActiveEditorTracker.sharedTracker.isLocked;
            ActiveEditorTracker.sharedTracker.ForceRebuild();
        }

        [MenuItem("Edit/Lock Inspector %l", true)]
        public static bool IsValid()
        {
            return ActiveEditorTracker.sharedTracker.activeEditors.Length > 0;
        }
    }
}