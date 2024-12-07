using UnityEngine;
using UnityEngine.InputSystem;

namespace Game {
	public class CameraTouchMovement: MonoBehaviour {
		[SerializeField] private Camera _camera;
		[SerializeField] private InputAction _position;
		[SerializeField] private InputAction _click;
		private Vector2 _pointerStart;
		private bool _delayedStart = false;
		
		private void Awake() {
			if (_camera == null) {
				_camera = Camera.main;
			}
		}
		private void LateUpdate() {
			if (_click.inProgress) {
				if (_delayedStart) {
					_pointerStart = _camera.ScreenToWorldPoint(_position.ReadValue<Vector2>());
					_delayedStart = false;
				}
				var pointer = _position.ReadValue<Vector2>();
				var delta = _pointerStart - (Vector2)_camera.ScreenToWorldPoint(pointer);
				_camera.transform.position += (Vector3)delta;
			}
		}
		private void OnClick(UnityEngine.InputSystem.InputAction.CallbackContext context) {
			var pointer = _position.ReadValue<Vector2>();
			if (pointer == Vector2.zero) {
				_delayedStart = true;
				return;
			}
			_pointerStart = _camera.ScreenToWorldPoint(pointer);
		}
		
		private void OnEnable() {
			_position.Enable();
			_click.Enable();
			_click.started += OnClick;
		}
		private void OnDisable() {
			_position.Disable();
			_click.Disable();
			_click.started -= OnClick;
		}
	}
}