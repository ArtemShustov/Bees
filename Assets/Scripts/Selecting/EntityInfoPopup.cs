using Game.Popups;
using TMPro;
using UnityEngine;

namespace Game.Selecting {
	public class EntityInfoPopup: Popup {
		[SerializeField] private TMP_Text _label;
		
		public void SetLabel(string text) => _label.text = text; 
	}
}