using Game.Registries;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Entities.Registry {
	public class EntityRegistry: IRegistry<EntityType> {
		public static readonly string Name = "Entities";

		private Dictionary<Type, EntityType> _map = new Dictionary<Type, EntityType>();

		public IReadOnlyCollection<EntityType> List => _map.Values.ToArray(); 

		public Identifier Register(EntityType item) {
			if (item.Prefab == null) {
				return null;
			}
			var key = item.Prefab.GetType();
			if (_map.ContainsKey(key) || Get(item.Id) != null) {
				return null;
			}
			_map[key] = item;
			return item.Id;
		}
		public EntityType Get<T>() where T: Entity {
			return _map.TryGetValue(typeof(T), out var item) ? item : null;
		}
		public EntityType Get(string id) {
			return _map.Values.FirstOrDefault(item => item.Id.Equals(id));
		}
		public EntityType Get(Identifier id) {
			return _map.Values.FirstOrDefault(item => item.Id.Equals(id));
		}
		public Identifier IdentifierOf(string id) {
			return List.FirstOrDefault(item => item.Id.Equals(id))?.Id;
		}
	}
}