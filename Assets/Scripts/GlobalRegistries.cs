using Game.Bees.Genome;
using Game.Entities.Registry;
using Game.Resources;
using UnityEngine;

namespace Game {
	public class GlobalRegistries {
		public static ItemRegistry Items { get; private set; } = new ItemRegistry();
		public static GenRegistry Genes { get; private set; } = new GenRegistry();
		public static EntityRegistry Entities { get; private set; } = new EntityRegistry();
		
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void LoadRegistries() {
			LoadAsset<ItemRegistryAsset>("Registries/" + ItemRegistry.Name).RegisterAll(Items);
			Debug.Log($"Loaded ItemRegistry with {Items.List.Count} items.");

			LoadAsset<GenRegistryAsset>("Registries/" + GenRegistry.Name).RegisterAll(Genes);
			Debug.Log($"Loaded GenRegistry with {Genes.List.Count} items.");

			LoadAsset<EntityRegistryAsset>("Registries/" + EntityRegistry.Name).RegisterAll(Entities);
			Debug.Log($"Loaded EntityRegistry with {Entities.List.Count} items.");
		}

		private static T LoadAsset<T>(string path) where T: ScriptableObject {
			var result = UnityEngine.Resources.Load<T>(path);
			if (result == null) {
				result = ScriptableObject.CreateInstance<T>();
			} else {
#if UNITY_EDITOR
				// Create copy of SO, because of unity.
				result = ScriptableObject.Instantiate(result);
#endif
			}
			return result;
		}
	}
}