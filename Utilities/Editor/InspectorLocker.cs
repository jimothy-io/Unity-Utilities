using System.Reflection;
using UnityEngine;
using UnityEditor;

namespace Jimothy.Utilities.Editor
{
    public static class InspectorLocker
    {
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