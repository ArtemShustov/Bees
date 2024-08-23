using UnityEngine;
using UnityEditor;
using Game.Resources;

namespace GameEditor.Resources {
	[CustomPropertyDrawer(typeof(ItemSearchAttribute))]
	public class ItemSearchDrawer: PropertyDrawer {
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {;
			if (property.propertyType != SerializedPropertyType.String) {
				EditorGUILayout.HelpBox("Can't display this property", MessageType.Error);
				return;
			}

			var atr = attribute as ItemSearchAttribute;
			EditorGUI.BeginProperty(position, label, property);
			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
			var indent = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 1;

			EditorGUILayout.PropertyField(property, new GUIContent("Id"));

			if (string.IsNullOrEmpty(property.stringValue)) {
				Item temp = null;
				var selection = EditorGUILayout.ObjectField(temp, typeof(Item), false);
				if (selection != null) {
					if (selection is Item item) {
						property.stringValue = item.Id.ToString();
					}
				}
			}

			EditorGUI.indentLevel = indent;
			EditorGUI.EndProperty();
		}
	}
}