using UnityEditor;
using UnityEngine;

namespace Game.Utils.Editor {
	[CustomPropertyDrawer(typeof(HideInPlayModeAttribute))]
	public class HideInPlayModeDrawer: PropertyDrawer {
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			if (!Application.isPlaying) {
				EditorGUI.PropertyField(position, property, label);
			}
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
			return Application.isPlaying ? 0 : EditorGUI.GetPropertyHeight(property, label);
		}
	}
}