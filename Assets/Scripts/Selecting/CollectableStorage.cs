using System.Linq;
using Game.Items;
using Game.Popups;
using UnityEngine;

namespace Game.Selecting {
	public class CollectableStorage: MonoBehaviour, IClickTarget {
		[SerializeField] private Popup _fullStoragePrefab;
		[SerializeField] private PopupRoot _popupRoot;
		[SerializeField] private LimitedStorage _container;
		
		[field: SerializeField] public int Order { get; private set; }
		
		public bool Click() {
			if (!_container.IsFull) {
				return false;
			}
			var items = _container.TakeAll();
			_popupRoot.Hide();
			Debug.Log($"Storage '{gameObject.name}' collected: {items.Sum(item => item.Count)}");
			return true;
		}

		private void OnFillChanged() {
			if (_container.IsFull) {
				_popupRoot.ShowFromPrefab(_fullStoragePrefab);
			}
		}
		private void OnEnable() {
			_container.FillChanged += OnFillChanged;
		}
		private void OnDisable() {
			_container.FillChanged -= OnFillChanged;
		}
	}
}