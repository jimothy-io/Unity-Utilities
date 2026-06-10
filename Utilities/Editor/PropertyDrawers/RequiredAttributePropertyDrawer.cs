using Jimothy.Utilities.Editor.Attributes;
using UnityEditor;
using UnityEngine;

namespace Jimothy.Utilities.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(RequiredAttribute))]
    public class RequiredAttributePropertyDrawer : PropertyDrawer
    {
        private static readonly Color ErrorColor = new(1f, 0.2f, 0.2f, 0.1f);

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (IsFieldEmpty(property))
            {
                float height = EditorGUIUtility.singleLineHeight * 2f;
                height += base.GetPropertyHeight(property, label);

                return height;
            }

            return base.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!IsFieldSupported(property))
            {
                Debug.LogError($"Unsupported field type for RequiredAttribute: {property.propertyType}");
                return;
            }

            if (IsFieldEmpty(property))
            {
                position.height = EditorGUIUtility.singleLineHeight * 2f;
                position.height += base.GetPropertyHeight(property, label);

                EditorGUI.HelpBox(position, "Required", MessageType.Error);
                EditorGUI.DrawRect(position, ErrorColor);

                position.height = base.GetPropertyHeight(property, label);
                position.y += EditorGUIUtility.singleLineHeight * 2f;
            }

            EditorGUI.PropertyField(position, property, label);
        }

        private bool IsFieldEmpty(SerializedProperty property)
        {
            if (property.propertyType == SerializedPropertyType.ObjectReference &&
                property.objectReferenceValue == null)
                return true;

            if (property.propertyType == SerializedPropertyType.String && string.IsNullOrEmpty(property.stringValue))
                return true;

            return false;
        }

        private bool IsFieldSupported(SerializedProperty property)
        {
            return property.propertyType is SerializedPropertyType.ObjectReference or SerializedPropertyType.String;
        }
    }
}