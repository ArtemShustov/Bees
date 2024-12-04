using System;
using UnityEngine;

namespace Game.UI {
	public class UIPanel: MonoBehaviour, IUIPanel {
		public event Action Closed;
		
		public virtual void Show() {
			gameObject.SetActive(true);
		}
		public virtual void Hide() {
			gameObject.SetActive(false);
			Closed?.Invoke();
		}
	}
}