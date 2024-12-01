using Game.Registries;
using UnityEngine;

namespace Game.Items {
	public class ItemRegistry: Registry<Item> {
		protected override void OnItemAdd(Item item) {
			Debug.Log($"Item added: {item.Id}");
		}
	}
}