using UnityEditor;
using UnityEngine;

namespace MainGame.Scripts
{
    [CustomPropertyDrawer(typeof(InterfaceFieldAttribute))]
    public class InterfaceFieldDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            InterfaceFieldAttribute interfaceField = (InterfaceFieldAttribute)attribute;

            if (property.propertyType != SerializedPropertyType.ObjectReference)
            {
                EditorGUI.LabelField(position, label.text, "Use [InterfaceField] with object references.");
                return;
            }

            if (property.objectReferenceValue != null && property.objectReferenceValue.Equals(null))
            {
                property.objectReferenceValue = null;
                return;
            }

            EditorGUI.BeginProperty(position, label, property);

            Object oldObj = property.objectReferenceValue;

            Object newObj = EditorGUI.ObjectField(position, label, oldObj, typeof(Object), true);

            if (newObj == null)
            {
                property.objectReferenceValue = null;
            }
            else if (interfaceField.InterfaceType.IsAssignableFrom(newObj.GetType()))
            {
                property.objectReferenceValue = newObj;
            }
            else if (newObj is GameObject go)
            {
                Component comp = go.GetComponent(interfaceField.InterfaceType);
                if (comp != null)
                {
                    property.objectReferenceValue = comp;
                }
            }
            else if (newObj is Component c && interfaceField.InterfaceType.IsAssignableFrom(c.GetType()))
            {
                property.objectReferenceValue = c;
            }

            EditorGUI.EndProperty();
        }
    }
}