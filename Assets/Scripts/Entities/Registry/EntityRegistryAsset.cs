using Game.Registries;
using UnityEngine;

namespace Game.Entities.Registry {
	[CreateAssetMenu(menuName = "Registry/Entities")]
	public class EntityRegistryAsset: RegistryAsset<EntityType> { }
}