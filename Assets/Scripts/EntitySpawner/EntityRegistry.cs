using Game.Registries;
using System.Linq;

namespace Game.EntitySpawner {
	public class EntityRegistry: Registry<EntityType> {
		public static readonly string Name = "Entities";

		public T Get<T>() where T: EntityType {
			return List.FirstOrDefault((item) => item.GetType() == typeof(T)) as T;
		}
	}
}