using UnityEngine;
using UnityEditor;
using Game.Resources;

namespace GameEditor.Resources {
	[CustomPropertyDrawer(typeof(ItemRegisterSearchAttribute))]
	public class ItemRegisterSearchDrawer: PropertyDrawer {
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			var atr = attribute as ItemRegisterSearchAttribute;
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