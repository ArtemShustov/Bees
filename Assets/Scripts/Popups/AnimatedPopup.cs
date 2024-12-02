using System;
using UnityEngine;

namespace Game.Popups {
	public class AnimatedPopup: Popup {
		[SerializeField] private Animator _animator;
		[SerializeField] private string _showTrigger = "Show";
		[SerializeField] private string _hideTrigger = "Hide";

		private Action _callback;
		
		public override void Show(Transform root, Action onReady = null) {
			base.Show(root, null);
			_callback = onReady;
			_animator.SetTrigger(_showTrigger);
		}
		public override void Hide(Action onReady = null) {
			base.Hide(null);
			_callback = onReady;
			_animator.SetTrigger(_hideTrigger);
		}

		public void OnAnimationEnd() {
			_callback?.Invoke();
		}
	}
}