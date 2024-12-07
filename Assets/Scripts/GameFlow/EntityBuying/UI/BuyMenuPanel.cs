using System;
using Game.UI;
using UnityEngine;

namespace Game.GameFlow.EntityBuying.UI {
	public class BuyMenuPanel: UIPanel {
		[SerializeField] private EntityBuyEntry[] _entryList;
		[SerializeField] private BuyListView _view;

		public event Action<EntityBuyEntry> Selected;

		private void OnEntrySelected(EntityBuyEntry entry) {
			Selected?.Invoke(entry);
		}
		private void OnEnable() {
			_view.Selected += OnEntrySelected;
			_view.Show(_entryList);
		}
		private void OnDisable() {
			_view.Selected -= OnEntrySelected;
		}
	}
}