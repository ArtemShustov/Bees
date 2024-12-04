using Game.Registries;
using Game.World;
using UnityEngine;

namespace Game.Entities.Registries {
	public class EntityRegistry: Registry<EntityType> {
		public Entity Spawn(Level level, Identifier id) {
			var prefab = Get(id);
			if (prefab == null) {
				return null;
			}
			var instance = prefab.SpawnAt(level.transform, Vector2.zero);
			level.AddEntity(instance);
			return instance;
		}
		
		protected override void OnItemAdd(EntityType item) {
			Debug.Log($"Entity added: {item.Id}");
		}
	}
}