using UnityEditor;
using UnityEngine;

namespace MainGame.Scripts
{
    [CustomPropertyDrawer(typeof(InterfaceFieldAttribute))]
    public class InterfaceFieldDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.ObjectReference)
            {
                EditorGUI.LabelField(position, label.text, "Use [InterfaceField] with object references.");
                return;
            }

            EditorGUI.BeginProperty(position, label, property);
            Object oldObj = property.objectReferenceValue;

            Object newObj = EditorGUI.ObjectField(position, label, oldObj, typeof(Object), true);

            if (newObj == null || ((InterfaceFieldAttribute)attribute).InterfaceType.IsAssignableFrom(newObj.GetType()))
            {
                property.objectReferenceValue = newObj;
            }
            else if (newObj is GameObject go)
            {
                Component comp = go.GetComponent(((InterfaceFieldAttribute)attribute).InterfaceType);
                if (comp != null)
                    property.objectReferenceValue = comp;
            }
            else if (newObj is Component c && ((InterfaceFieldAttribute)attribute).InterfaceType.IsAssignableFrom(c.GetType()))
            {
                property.objectReferenceValue = c;
            }

            EditorGUI.EndProperty();
        }
    }
}