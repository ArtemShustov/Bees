using Game.Selecting;
using Game.UI;
using UnityEngine;

namespace Game.GameFlow.States {
	public class FreeModeState: GameState {
		[SerializeField] private UIPanel _panel;
		[SerializeField] private PanelSwitcher _switcher;
		[SerializeField] private PointerClickSelector _selector;
		[SerializeField] private GameObject _cameraMovement;

		private void Awake() {
			_selector.enabled = false;
		}

		public override void OnEnter() {
			_selector.enabled = true;
			_switcher.Switch(_panel);
			_cameraMovement.SetActive(true);
		}
		public override void OnExit() {
			_selector.enabled = false;
			_cameraMovement.SetActive(false);
		}
	}
}