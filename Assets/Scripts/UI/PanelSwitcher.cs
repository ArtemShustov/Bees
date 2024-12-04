using UnityEngine;

namespace Game.UI {
	public class PanelSwitcher: MonoBehaviour {
		[SerializeField] private bool _disableChildsOnStart;
		[SerializeField] private UIPanel _defaultPanel;
		
		private IUIPanel _current;

		public IUIPanel Current => _current;
		
		private void Awake() {
			if (_disableChildsOnStart) {
				foreach (var child in transform) {
					(child as Transform)?.gameObject.SetActive(false);
				}
			}
		}

		public void Switch(IUIPanel panel) {
			_current?.Hide();
			_current = panel;
			_current?.Show();
		}
		public void SwitchTo(UIPanel panel) {
			Switch(panel);
		}
		public void SwitchDefault() {
			Switch(_defaultPanel);
		}
	}
}