using System;
using UnityEngine;

namespace Game.Popups {
	public class Popup: MonoBehaviour {
		public virtual void Show(Transform root, Action onReady = null) {
			transform.SetParent(root);
			transform.localPosition = Vector3.zero;
			gameObject.SetActive(true);
			onReady?.Invoke();
		}
		public virtual void Hide(Action onReady = null) {
			gameObject.SetActive(false);
			onReady?.Invoke();
		}
	}

}