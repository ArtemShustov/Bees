using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

namespace Game.UI {
	public static class UiUtils {
		public static bool IsPointerOverUI() {
			if (EventSystem.current == null) {
				return false;
			}
			RaycastResult lastRaycastResult = ((InputSystemUIInputModule)EventSystem.current.currentInputModule).GetLastRaycastResult(Mouse.current.deviceId);
			const int uiLayer = 5;
			return lastRaycastResult.gameObject != null && lastRaycastResult.gameObject.layer == uiLayer;
		}
	}
}