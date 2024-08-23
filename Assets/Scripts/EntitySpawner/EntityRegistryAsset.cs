using Game.Registries;
using UnityEngine;

namespace Game.EntitySpawner {
	[CreateAssetMenu(menuName = "Registry/Entities")]
	public class EntityRegistryAsset: RegistryAsset<EntityType> { }
}