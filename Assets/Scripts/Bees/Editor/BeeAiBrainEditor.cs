using UnityEditor;
using UnityEngine;

namespace Game.Bees.Editor {
	[CustomEditor(typeof(BeeAiBrain))]
	public class BeeAiBrainEditor: UnityEditor.Editor {
		public override void OnInspectorGUI() {
			DrawDefaultInspector();

			if (!Application.isPlaying) {
				return;
			} 
			var ai = target as BeeAiBrain;
			EditorGUILayout.Space();
			EditorGUILayout.Separator();

			GUILayout.Label($"Goal: {ai.GoalSelector.GetCurrent()?.GetType()}");
			if (GUILayout.Button("Reset timer")) {
				ai.ResetTimer();
			}
			if (GUILayout.Button("Clear home & flower")) {
				ai.SetHome(null);
				ai.SetFlower(null);
			}
		}
	}
}