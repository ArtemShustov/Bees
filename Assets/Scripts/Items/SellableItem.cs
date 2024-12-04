using UnityEngine;

namespace Game.Items {
	[CreateAssetMenu(menuName = "Items/Sellable")]
	public class SellableItem: Item {
		[field: SerializeField] public int SellPrice { get; private set; }
	}
}