using Game.Popups;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Selecting {
	public class EntityInfo: MonoBehaviour, IClickSelectableTarget {
		[SerializeField] private string _label;
		[FormerlySerializedAs("_popup")]
		[SerializeField] private EntityInfoPopup _prefab;
		[SerializeField] private PopupRoot _popupRoot;
		
		[field: SerializeField] public int Order { get; private set; }

		public bool Click() {
			var popup = Instantiate(_prefab);
			popup.SetLabel(_label);
			_popupRoot.ShowExisting(popup);
			Debug.Log($"EntityInfo '{gameObject.name}' clicked.");
			return true;
		}
		public void OnUnselected() {
			_popupRoot.Hide();
			Debug.Log($"EntityInfo '{gameObject.name}' unselected.");
		}
	}
}