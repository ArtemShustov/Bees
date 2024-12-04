using UnityEngine;

namespace Game.UI {
	public class SetDefaultPanel: MonoBehaviour {
		[SerializeField] private PanelSwitcher _switcher;
		[SerializeField] private UIPanel _panel;

		private void Start() {
			_switcher.Switch(_panel);
		}
	}
}