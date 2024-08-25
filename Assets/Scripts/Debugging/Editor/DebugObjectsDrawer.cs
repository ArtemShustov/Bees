using UnityEditor;
using UnityEngine;

namespace Game.Debugging.Editor {
	public static class DebugObjectsDrawer {
		[DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
		private static void DrawInfo(DebugObject debugObject, GizmoType gizmo) {
			if (debugObject == null || !Application.isPlaying) {
				return;
			}
			var position = debugObject.transform.position;
			GUIStyle style = new GUIStyle();
			style.normal.textColor = Color.red;
			style.fontSize = gizmo switch {
				GizmoType.Selected | GizmoType.Active | GizmoType.InSelectionHierarchy => 18,
				_ => 8,
			}; ; ;
			style.fontStyle = FontStyle.Bold;

			var label = debugObject.GetInfo();

			label = $"{gizmo}\n" + label;

			Handles.Label(position, label, style);
		}
	}
}