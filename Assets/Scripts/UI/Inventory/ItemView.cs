using Game.Items;
using TMPro;
using UnityEngine;

namespace Game.UI.Inventory {
	public class ItemView: MonoBehaviour {
		[SerializeField] private ItemIcon _icon;
		[SerializeField] private TMP_Text _name;

		public void Set(ItemStack stack) {
			_icon.SetItem(stack.Item);
			_name.text = $"{stack.Count}x{stack.Item.Id}";
		}
	}
}