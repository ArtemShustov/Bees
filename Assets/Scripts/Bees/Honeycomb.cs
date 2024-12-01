using Game.Items;
using UnityEngine;

namespace Game.Bees {
	[CreateAssetMenu(menuName = "Items/Honeycomb")]
	public class Honeycomb: Item, IColorableIcon {
		[field: SerializeField] public Color Color { get; private set; } = Color.white;
	}
}