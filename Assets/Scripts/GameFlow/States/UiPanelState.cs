using Game.UI;
using UnityEngine;

namespace Game.GameFlow.States {
	public class UiPanelState: GameState {
		[SerializeField] private UIPanel _panel;
		[SerializeField] private PanelSwitcher _switcher;
		[SerializeField] private GameStateMachine _machine;

		public void ChangeStateToThis() {
			_machine.ChangeTo(this);
		}

		public override void OnEnter() {
			_switcher.Switch(_panel);
		}
		public override void OnExit() {
			//
		}
	}
}