using Source.PropertyAttributes;
using UnityEditor;
using UnityEngine;

namespace Source.Editor
{
    [CustomPropertyDrawer(typeof(InterfaceTooltipAttribute))]
    public class InterfaceTooltipAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property,
            GUIContent label)
        {
            var interfaceAttribute = (InterfaceTooltipAttribute) attribute;
            label.text = property.displayName + $"({interfaceAttribute.Name})";
            EditorGUILayout.PropertyField(property, label);
        }
    }
}