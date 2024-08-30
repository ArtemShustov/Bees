using Game.GameStates;
using UnityEngine;

namespace Game.UI {
	public class ToggleMenu: MonoBehaviour {
		[SerializeField] private GameStateSwitch _stateSwitch;

		private void Toggle(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
			Toggle();
		}
		public void Toggle() {
			_stateSwitch.Change(_stateSwitch.Current is MenuState ? _stateSwitch.InGame : _stateSwitch.Menu);
		}

		private void OnEnable() {
			GlobalInput.Actions.Common.ToggleMenu.performed += Toggle;
		}
		private void OnDisable() {
			GlobalInput.Actions.Common.ToggleMenu.performed -= Toggle;
		}
	}
}