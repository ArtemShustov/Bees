using Game.UI;
using UnityEngine;

namespace Game.EntityBuying.UI {
	public class BuyMenuPanel: UIPanel {
		[SerializeField] private EntityBuyEntry[] _entryList;
		[SerializeField] private BuyListView _view;
		[SerializeField] private PlaceEntityPanel _placeEntityPanel;
		[SerializeField] private PanelSwitcher _switcher;
		[SerializeField] private Wallet _wallet;

		public void SwitchToThis() {
			_switcher.Switch(this);
			_view.Show(_entryList);
		}

		private void OnEntrySelected(EntityBuyEntry entry) {
			if (_wallet.TryTake(entry.Cost)) {
				_placeEntityPanel.SetEntity(entry.Entity);
				_placeEntityPanel.SwitchToThis();
			}
		}
		private void OnEnable() {
			_view.Selected += OnEntrySelected;
		}
		private void OnDisable() {
			_view.Selected -= OnEntrySelected;
		}
	}
}