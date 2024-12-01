using Game.Registries;
using Game.World;
using UnityEngine;

namespace Game.Entities.Registries {
	public class EntityRegistry: Registry<Entity> {
		public Entity Spawn(Level level, Identifier id) {
			var prefab = Get(id);
			if (prefab == null) {
				return null;
			}
			var instance = GameObject.Instantiate(prefab);
			level.AddEntity(instance);
			return instance;
		}
		
		protected override void OnItemAdd(Entity item) {
			Debug.Log($"Entity added: {item.Id}");
		}
	}
}