using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Game.Selecting {
	public class PointerClickSelector: MonoBehaviour {
		[FormerlySerializedAs("_dist")]
		[SerializeField] private float _checkRadius = 0.1f;
		[SerializeField] private InputAction _clickAction;
		[SerializeField] private Camera _camera;

		private IClickSelectableTarget _current;
		
		private bool ClickUnderPointer() {
			var position = (Vector2)_camera.ScreenToWorldPoint(Input.mousePosition);
			var clickableObjects = Physics2D.OverlapCircleAll(position, _checkRadius)
				.Where(c => c.TryGetComponent<IClickTarget>(out var _));

			var clicked = ClickFirst(clickableObjects);
			if (clicked != null) {
				if (_current == clicked) {
					return true;
				}
				_current?.OnUnselected();
				_current = null;
				if (clicked is IClickSelectableTarget target) {
					_current = target;
				}
				return true;
			} else {
				if (_current != null) {
					_current.OnUnselected();
					_current = null;
				}
			}
			return false;
		}
		private IClickTarget ClickFirst(IEnumerable<Collider2D> clickableObjects) {
			foreach (var clickableObject in clickableObjects) {
				var components = clickableObject.gameObject.GetComponents<IClickTarget>();
				var clicked = components
					.OrderBy(c => c.Order)
					.ToArray()
					.FirstOrDefault(c => c.Click());
				if (clicked != null) {
					return clicked;
				}
			}
			return null;
		}

		private void OnClick(InputAction.CallbackContext obj) {
			var result = ClickUnderPointer();
		}
		private void OnEnable() {
			_clickAction.Enable();
			_clickAction.performed += OnClick;
		}
		private void OnDisable() {
			_clickAction.Disable();
			_clickAction.performed -= OnClick;
		}
	}
}