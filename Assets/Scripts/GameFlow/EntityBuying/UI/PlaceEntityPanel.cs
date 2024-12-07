using Game.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.GameFlow.EntityBuying.UI {
	public class PlaceEntityPanel: UIPanel {
		[SerializeField] private Button _placeButton;
		[SerializeField] private Button _cancelButton;
		
		public event UnityAction PlaceClicked {
			add => _placeButton.onClick.AddListener(value);
			remove => _placeButton.onClick.RemoveListener(value);
		} 
		public event UnityAction CancelClicked {
			add => _cancelButton.onClick.AddListener(value);
			remove => _cancelButton.onClick.RemoveListener(value);
		}

		public void SetCanPlace(bool canPlace) {
			_placeButton.interactable = canPlace;
		}
	}
}