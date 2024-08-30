using UnityEngine;

namespace Game.CameraControl {
	public class CameraZoom: MonoBehaviour {
		[SerializeField] private float _minSize = 1;
		[SerializeField] private float _maxSize = 15;
		[SerializeField] private float _sensitivity = 1;
		[SerializeField] private Camera _camera;

		private void Awake() {
			if (_camera == null) {
				_camera = Camera.main;
			}
		}
		private void Update() {
			if (GlobalInput.Actions.Gameplay.CameraZoom.inProgress) {
				var input = GlobalInput.Actions.Gameplay.CameraZoom.ReadValue<float>() * Time.deltaTime;
				_camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize + input * _sensitivity, _minSize, _maxSize);
			}
		}
	}
}