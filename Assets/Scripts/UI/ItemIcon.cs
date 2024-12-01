using Game.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI {
	public class ItemIcon: MonoBehaviour {
		[SerializeField] private Image _image;
		[SerializeField] private Item _item;
		
		public void SetItem(Item item) {
			_item = item;
			_image.sprite = _item.Icon;
			if (_item is IColorableIcon colorableIcon) {
				_image.color = colorableIcon.Color;
			} else {
				_image.color = Color.white;
			}
		}

		private void OnValidate() {
			SetItem(_item);
		}
	}
}