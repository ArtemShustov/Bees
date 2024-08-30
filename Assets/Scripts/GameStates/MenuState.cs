using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.GameStates {
	public class MenuState: GameState {
		[SerializeField] private GameObject _defaultSelected;
		[SerializeField] private GameObject _ui;

		private GameStateSwitch _current;

		public override void OnEnter(GameStateSwitch stateSwitch) {
			_current = stateSwitch;
			_ui.SetActive(true);
			EventSystem.current.SetSelectedGameObject(_defaultSelected);
			GlobalInput.Actions.UI.Enable();
		}
		public override void OnExit(GameStateSwitch stateSwitch) {
			_ui.SetActive(false);
			GlobalInput.Actions.UI.Disable();
		}

		public void Quit() {
			Application.Quit();
		}
		public void OpenItch() {
			Application.OpenURL("https://artemshustov.itch.io");
		}
	}
}