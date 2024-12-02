using UnityEditor;
using UnityEngine;

namespace Game.World.Editor {
	[CustomEditor(typeof(Ticker))]
	public class TickerEditor: UnityEditor.Editor {
		public override void OnInspectorGUI() {
			DrawDefaultInspector();
			if (!Application.isPlaying) {
				return;
			}

			var ticker = target as Ticker;

			if (GUILayout.Button(ticker.enabled ? "Disable" : "Enable")) {
				ticker.enabled = !ticker.enabled;
			}
			if (GUILayout.Button("Tick all")) {
				ticker.TickAll();
			}
		}
	}
}