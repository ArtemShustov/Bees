using System;
using Game.Player;
using UnityEngine;

namespace Game.GameFlow.EntityBuying.UI {
	public class BuyListView: MonoBehaviour {
		[SerializeField] private BuyEntryView _prefab;
		[SerializeField] private Transform _container;
		[SerializeField] private PlayerMoneyWallet _wallet;

		public event Action<EntityBuyEntry> Selected;
		
		public void Show(EntityBuyEntry[] items) {
			Clear();

			foreach (var buyEntry in items) {
				var instance = Instantiate(_prefab, _container);
				instance.SetItem(buyEntry, _wallet);
				instance.Clicked += OnEntryClicked;
			}
		}
		public void Clear() {
			while (_container.childCount > 0) {
				var child = _container.GetChild(0);
				child.SetParent(null);
				Destroy(child.gameObject);
			}
		}

		private void OnEntryClicked(BuyEntryView entryView) {
			Selected?.Invoke(entryView.Item);
		}
	}

}