using UnityEditor;
using UnityEngine;

namespace Game.Popups.Editor {
	[CustomEditor(typeof(PopupRoot))]
	public class PopupRootEditor: UnityEditor.Editor {
		private Popup _test;
		
		public override void OnInspectorGUI() {
			DrawDefaultInspector();
			
			if (!Application.isPlaying) {
				return;
			}
			EditorGUILayout.Separator();
			PopupRoot root = (PopupRoot)target;
			GUILayout.Label($"IsShowed: {root.IsShowed}");
			_test = EditorGUILayout.ObjectField("Test popup", _test, typeof(Popup), false) as Popup;
			if (GUILayout.Button("Show")) {
				root.ShowFromPrefab(_test);
			}
			if (GUILayout.Button("Hide")) {
				root.Hide();
			}
		}
	}
}