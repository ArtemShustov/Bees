using UnityEngine;

namespace Game.Popups {
	public class PopupRoot: MonoBehaviour {
		[SerializeField] private Transform _root;

		private Popup _current;
		
		public bool IsShowed => _current;
		public Popup Current => _current;

		public void ShowFromPrefab(Popup prefab) {
			var instance = Instantiate(prefab, _root);
			ShowExisting(instance);
		}
		public void ShowExisting(Popup instance) {
			if (_current) {
				_current.Hide();
				if (_current) {
					Destroy(_current.gameObject);
				}
			}
			_current = instance;
			_current.Show(_root);
		}
		public void Hide() {
			if (_current == null) {
				return;
			}
			_current.Hide(() => {
				if (_current) Destroy(_current.gameObject);
			});
		}
	}
}