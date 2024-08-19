using Game.Resources;
using UnityEngine;

namespace Game {
	public class GlobalRegistries {
		public static ItemRegistry Items { get; private set; }
		
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void LoadRegistries() {
			Items = LoadAsset<ItemRegistry>("Registries/" + ItemRegistry.Name);
			Debug.Log($"Loaded ItemRegistry with {Items.List.Count} items.");
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