using UnityEngine;

namespace Game.CameraControl {
	public class CameraTouchMovement: MonoBehaviour {
		[SerializeField] private Camera _camera;

		private Vector2 _pointerStart;
		private bool _delayedStart = false;

		private void Awake() {
			if (_camera == null) {
				_camera = Camera.main;
			}
		}

		private void LateUpdate() {
			if (GlobalInput.Actions.Gameplay.PointerClick.inProgress) {
				if (_delayedStart) {
					_pointerStart = _camera.ScreenToWorldPoint(GlobalInput.Actions.Gameplay.PointerPosition.ReadValue<Vector2>());
					_delayedStart = false;
				}
				var pointer = GlobalInput.Actions.Gameplay.PointerPosition.ReadValue<Vector2>();
				var delta = _pointerStart - (Vector2)_camera.ScreenToWorldPoint(pointer);
				_camera.transform.position += (Vector3)delta;
			}
		}
		private void OnClick(UnityEngine.InputSystem.InputAction.CallbackContext context) {
			var pointer = GlobalInput.Actions.Gameplay.PointerPosition.ReadValue<Vector2>();
			if (pointer == Vector2.zero) {
				_delayedStart = true;
				return;
			}
			_pointerStart = _camera.ScreenToWorldPoint(pointer);
		}
		
		private void OnEnable() {
			GlobalInput.Actions.Gameplay.PointerClick.started += OnClick;
		}
		private void OnDisable() {
			GlobalInput.Actions.Gameplay.PointerClick.started -= OnClick;
		}
	}
}