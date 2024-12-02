using Game.Bees.Genes;
using Game.Entities.Registries;
using Game.Items;
using UnityEngine;

namespace Game.Registries {
	public static class GlobalRegistries {
		public readonly static EntityRegistry Entities = new EntityRegistry();
		public readonly static string EntitiesPath = "Registries/Entities";
		
		public readonly static ItemRegistry Items = new ItemRegistry();
		public readonly static string ItemsPath = "Registries/Items";
		
		public readonly static GeneRegistry Genes = new GeneRegistry();
		public readonly static string GenesPath = "Registries/Genes";

		public static bool IsInitialized { get; private set; } = false;
		
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void Load() {
			Resources.Load<EntityRegistryAsset>(EntitiesPath)?.RegisterAll(Entities);
			Debug.Log($"Registed {Entities.List.Count} entities.");
			Resources.Load<ItemRegistryAsset>(ItemsPath)?.RegisterAll(Items);
			Debug.Log($"Registed {Items.List.Count} items.");
			Resources.Load<GeneRegesitryAsset>(GenesPath)?.RegisterAll(Genes);
			Debug.Log($"Registed {Genes.List.Count} genes.");

			IsInitialized = true;
		}
		private static T LoadAsset<T>(string path) where T: ScriptableObject {
			var result = UnityEngine.Resources.Load<T>(path);
			if (result == null) {
				result = ScriptableObject.CreateInstance<T>();
			}
			return result;
		}
	}
}