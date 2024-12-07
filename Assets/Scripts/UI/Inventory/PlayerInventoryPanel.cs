using UnityEngine;

namespace Game.UI.Inventory {
	public class PlayerInventoryPanel: UIPanel {
		[SerializeField] private StorageView _view;
		[SerializeField] private PanelSwitcher _switcher;

		private void OnEnable() {
			_view.Show();
		}
	}
}