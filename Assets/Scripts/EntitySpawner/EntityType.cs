using Game.Registries;
using Game.World;
using UnityEngine;

namespace Game.EntitySpawner {
	public abstract class EntityType: ScriptableObject, IRegistryItem {
		[field: SerializeField] public Identifier Id { get; private set; }

		public abstract GameObject Spawn(Level level);
	}
}