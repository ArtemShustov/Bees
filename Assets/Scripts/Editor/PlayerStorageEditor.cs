using UnityEditor;
using UnityEngine;

namespace Game.Editor {
	[CustomEditor(typeof(PlayerStorage))]
	public class PlayerStorageEditor: UnityEditor.Editor {
		private bool _foldout;
		
		public override void OnInspectorGUI() {
			DrawDefaultInspector();

			if (!Application.isPlaying) {
				return;
			}
			var storage = (PlayerStorage)target;
			EditorGUILayout.Separator();
			_foldout = EditorGUILayout.BeginFoldoutHeaderGroup(_foldout, "Items");
			if (_foldout) {
				EditorGUI.indentLevel += 1;
				if (storage.Items.Count == 0) {
					GUILayout.Label("Empty");
				}
				foreach (var stack in storage.Items) {
					GUILayout.Label($"{stack.Count} of {stack.Item.Id}");
				}
				EditorGUI.indentLevel -= 1;
			}
			EditorGUILayout.EndFoldoutHeaderGroup();
		}
	}
}