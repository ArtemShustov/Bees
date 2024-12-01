using System.IO;
using UnityEditor;
using UnityEngine;

namespace Game.Debugging.Editor {
	public static class DebugInfoDrawer {
		[DrawGizmo(GizmoType.Selected)]
		private static void DrawEntityInfo(Transform gameObject, GizmoType gizmo) {
			if (gameObject == null || !Application.isPlaying) {
				return;
			}
			var position = gameObject.transform.position;
			GUIStyle style = new GUIStyle();
			style.normal.textColor = Color.red;
			style.alignment = TextAnchor.UpperCenter;
			style.fontSize = 18;

			var providers = gameObject.GetComponents<IDebugInfoProvider>();
			if (providers.Length != 0) {
				using var writer = new StringWriter();

				foreach (var provider in providers) {
					provider.GetDebugInfo(writer);
				}
				
				Handles.Label(position, writer.ToString(), style);
			}
		}
	}
}