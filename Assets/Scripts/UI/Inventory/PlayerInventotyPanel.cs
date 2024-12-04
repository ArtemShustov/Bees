using UnityEngine;

namespace Game.UI.Inventory {
	public class PlayerInventotyPanel: UIPanel {
		[SerializeField] private StorageView _view;
		[SerializeField] private PanelSwitcher _switcher;

		public void SwitchToThis() {
			_switcher.Switch(this);
			_view.Show();
		}
	}
}