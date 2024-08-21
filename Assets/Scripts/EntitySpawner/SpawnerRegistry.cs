using Game.Registries;
using System.Linq;

namespace Game.EntitySpawner {
	public class SpawnerRegistry: Registry<Spawner> {
		public static readonly string Name = "Spawners";

		public T Get<T>() where T: Spawner {
			return List.FirstOrDefault((item) => item.GetType() == typeof(T)) as T;
		}
	}
}