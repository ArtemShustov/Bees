using System;
using Game.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Inventory {
	public class StorageView: MonoBehaviour {
		[SerializeField] private Storage _storage;
		[SerializeField] private ItemView _itemPrefab;
		[SerializeField] private Transform _container;
		[SerializeField] private Button _sellButton;
		[SerializeField] private Wallet _wallet;

		public void Show() {
			while (_container.childCount > 0) {
				var child = _container.GetChild(0);
				child.SetParent(null);
				Destroy(child);
			}

			foreach (var stack in _storage.Items) {
				var instance = Instantiate(_itemPrefab, _container);
				instance.Set(stack);
			}
			
			_sellButton.interactable = _storage.Items.Count > 0;
		}

		private void SellAll() {
			var money = 0;
			foreach (var stack in _storage.TakeAll()) {
				if (stack.Item is SellableItem item) {
					money += item.SellPrice * stack.Count;
				}
			}
			_wallet.Add(money);
		}
		private void OnEnable() {
			_sellButton.onClick.AddListener(SellAll);
		}
		private void OnDisable() {
			_sellButton.onClick.RemoveListener(SellAll);
		}
	}
}