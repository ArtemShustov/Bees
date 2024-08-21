using Game.Bees.Genome;
using Game.EntitySpawner;
using Game.Resources;
using UnityEngine;

namespace Game {
	public class GlobalRegistries {
		public static ItemRegistry Items { get; private set; }
		public static GenRegistry Genes { get; private set; }
		public static SpawnerRegistry Spawners { get; private set; }
		
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void LoadRegistries() {
			Items = LoadAsset<ItemRegistry>("Registries/" + ItemRegistry.Name);
			Debug.Log($"Loaded ItemRegistry with {Items.List.Count} items.");

			Genes = LoadAsset<GenRegistry>("Registries/" + GenRegistry.Name);
			Debug.Log($"Loaded GenRegistry with {Genes.List.Count} items.");

			Spawners = LoadAsset<SpawnerRegistry>("Registries/" + SpawnerRegistry.Name);
			Debug.Log($"Loaded SpawnerRegistry with {Spawners.List.Count} items.");
		}
		private static T LoadAsset<T>(string path) where T: ScriptableObject {
			var result = UnityEngine.Resources.Load<T>(path);
			if (result == null) {
				ScriptableObject.CreateInstance<T>();
			}
			return result;
		}
	}
}