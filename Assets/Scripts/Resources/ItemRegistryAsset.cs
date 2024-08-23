using Game.Registries;
using UnityEngine;

namespace Game.Resources {
	[CreateAssetMenu(menuName = "Registry/Items")]
	public class ItemRegistryAsset: RegistryAsset<Item> { }
}