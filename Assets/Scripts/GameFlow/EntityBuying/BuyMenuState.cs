using Game.GameFlow.EntityBuying.UI;
using Game.UI;
using UnityEngine;

namespace Game.GameFlow.EntityBuying {
	public class BuyMenuState: GameState {
		[Header("UI")]
		[SerializeField] private BuyMenuPanel _panel;
		[SerializeField] private PanelSwitcher _switcher;
		[Header("States")]
		[SerializeField] private EntityPlaceState _placeState;
		[SerializeField] private GameStateMachine _machine;
		
		private void OnSelected(EntityBuyEntry selection) {
			_placeState.SetEntry(selection);
			_machine.ChangeTo(_placeState);
		}
		public override void OnEnter() {
			_switcher.Switch(_panel);
			_panel.Selected += OnSelected;
		}
		public override void OnExit() {
			_switcher.SwitchDefault();
		}
	}
}