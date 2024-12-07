using System.Collections.Generic;
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
			RaycastResult lastRaycastResult = ((InputSystemUIInputModule)EventSystem.current.currentInputModule).GetLastRaycastResult(Pointer.current.deviceId);
			
			const int uiLayer = 5;
			return lastRaycastResult.gameObject != null && lastRaycastResult.gameObject.layer == uiLayer;
		}
		public static bool IsOverUI(Vector2 screenPosition){
			if( EventSystem.current == null) {
				return false;
			}
    
			PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
			eventDataCurrentPosition.position = screenPosition;
    
			List<RaycastResult> results = new List<RaycastResult>();
			EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        
			return results.Count > 0;
		}
	}
}