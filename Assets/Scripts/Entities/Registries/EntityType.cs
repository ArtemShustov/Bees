using Game.Registries;
using UnityEngine;

namespace Game.Entities.Registries {
	[CreateAssetMenu(menuName = "Registries/Type/Entity")]
	public class EntityType: ScriptableObject, IRegistryItem {
		[SerializeField] private Entity _prefab;
		
		public Identifier Id => _prefab.Id;

		public Entity SpawnAt(Transform root, Vector2 position) {
			var instance = Spawn();
			instance.transform.SetParent(root);
			instance.transform.position = position;
			return instance;
		}
		public Entity SpawnAt(Vector2 position) {
			var instance = Spawn();
			instance.transform.position = position;
			return instance;
		}
		public Entity SpawnAt(Transform root) {
			var instance = Spawn();
			instance.transform.SetParent(root);
			return instance;
		}
		public virtual Entity Spawn() {
			return Instantiate(_prefab);
		}
	}
}