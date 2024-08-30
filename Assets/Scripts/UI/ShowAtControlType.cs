using UnityEngine;

namespace Game.UI {
	public class ShowAtControlType: MonoBehaviour {
		[SerializeField] private ControlType _targetControl;
		[SerializeField] private GameObject _object;

		private void Toggle(ControlType controlType) {
			_object.SetActive(controlType == _targetControl);
		}

		private void OnEnable() {
			GlobalInput.Current.ActiveControlChanged += Toggle;
			Toggle(GlobalInput.Current.ActiveControl);
		}
		private void OnDisable() {
			GlobalInput.Current.ActiveControlChanged -= Toggle;
		}
	}
}