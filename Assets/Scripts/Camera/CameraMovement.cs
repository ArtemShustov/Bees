using UnityEngine;

namespace Game.CameraControl {
	public class CameraMovement: MonoBehaviour {
		[SerializeField] private float _maxSpeed = 2;
		[SerializeField] private Camera _camera;

		private void Awake() {
			if (_camera == null) {
				_camera = Camera.main;
			}
		}

		private void LateUpdate() {
			if (GlobalInput.Actions.Gameplay.CameraMove.inProgress) {
				var input = GlobalInput.Actions.Gameplay.CameraMove.ReadValue<Vector2>();
				_camera.transform.position += Vector3.ClampMagnitude(input, 1) * _maxSpeed * Time.deltaTime;
			}
		}
	}
}