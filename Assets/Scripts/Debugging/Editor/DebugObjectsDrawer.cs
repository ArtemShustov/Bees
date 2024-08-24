using UnityEditor;
using UnityEngine;

namespace Game.Debugging.Editor {
	public static class DebugObjectsDrawer {
		[DrawGizmo(GizmoType.Selected)]
		private static void DrawInfo(DebugObject debugObject, GizmoType gizmo) {
			if (debugObject == null || !Application.isPlaying) {
				return;
			}
			var position = debugObject.transform.position;
			GUIStyle style = new GUIStyle();
			style.normal.textColor = Color.red;
			style.fontSize = 18;
			style.fontStyle = FontStyle.Bold;

			var label = debugObject.GetInfo();

			Handles.Label(position, label, style);
		}
	}
}