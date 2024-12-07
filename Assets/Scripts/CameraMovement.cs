using UnityEngine;
using UnityEngine.InputSystem;

namespace Game {
	public class CameraMovement: MonoBehaviour {
		[SerializeField] private InputAction _cameraMove;
		[SerializeField] private float _maxSpeed = 2;
		[SerializeField] private Camera _camera;
		
		private void Awake() {
			if (_camera == null) {
				_camera = Camera.main;
			}
		}
		private void LateUpdate() {
			if (_cameraMove.inProgress) {
				var input = _cameraMove.ReadValue<Vector2>();
				_camera.transform.position += Vector3.ClampMagnitude(input, 1) * _maxSpeed * Time.deltaTime;
			}
		}
	}
}