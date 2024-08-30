using UnityEngine;

namespace Game.GameStates {
	public class InGameState: GameState {
		[SerializeField] private GameObject _ui;

		public override void OnEnter(GameStateSwitch stateSwitch) {
			_ui.SetActive(true);
			GlobalInput.Actions.Gameplay.Enable();
		}
		public override void OnExit(GameStateSwitch stateSwitch) {
			_ui.SetActive(false);
			GlobalInput.Actions.Gameplay.Disable();
		}
	}
}