using UnityEngine;
using UnityEditor;
using Game.Resources;

namespace GameEditor.Resources {
	[CustomPropertyDrawer(typeof(ItemRegistrySearchAttribute))]
	public class ItemRegistrySearchDrawer: PropertyDrawer {
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			var atr = attribute as ItemRegistrySearchAttribute;
			EditorGUI.BeginProperty(position, label, property);
			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
			var indent = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 1;

			EditorGUILayout.PropertyField(property, new GUIContent("Id"));

			EditorGUI.indentLevel = indent;
			EditorGUI.EndProperty();
		}
	}
}