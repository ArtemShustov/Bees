using UnityEditor;
using UnityEngine.UIElements;
using UnityEngine;
using System.IO;
using Game.EntitySpawner;

namespace GameEditor.Resources {
	public class SpawnerRegistrySettingsProvider: SettingsProvider {
		private SpawnerRegistry _register;
		private SerializedObject _serialized;

		public SpawnerRegistrySettingsProvider() : base("Registries/Spawners", SettingsScope.Project) {
			_register = AssetDatabase.LoadAssetAtPath<SpawnerRegistry>("Assets/Resources/Registries/" + SpawnerRegistry.Name + ".asset");
			if (_register == null) {
				if (AssetDatabase.IsValidFolder("Assets/Resources/Registries") == false) {
					AssetDatabase.CreateFolder("Assets", "Resources");
					AssetDatabase.CreateFolder("Assets/Resources", "Registries");
				}
				_register = ScriptableObject.CreateInstance<SpawnerRegistry>();
				AssetDatabase.CreateAsset(_register, "Assets/Resources/Registries/" + SpawnerRegistry.Name + ".asset");
			}
		}

		[SettingsProvider]
		public static SettingsProvider CreateSettingsProvider() {
			var provider = new SpawnerRegistrySettingsProvider();
			return provider;
		}
		public override void OnActivate(string searchContext, VisualElement rootElement) {
			_serialized = new SerializedObject(_register);
		}
		public override void OnGUI(string searchContext) {
			if (GUILayout.Button("Update from 'Assets/Data/Spawners'")) {
				Debug.Log("Updating SpawnerRegistry...");
				LoadFromData();
				_serialized = new SerializedObject(_register);
			}

			EditorGUILayout.PropertyField(_serialized.FindProperty("_list"));
			_serialized.ApplyModifiedPropertiesWithoutUndo();
		}

		private void LoadFromData() {
			if (!AssetDatabase.IsValidFolder("Assets/Data/Spawners")) {
				return;
			}
			if (_register == null) {
				return;
			}
			var files = Directory.GetFiles("Assets/Data/Spawners", "*.asset", SearchOption.AllDirectories);
			var count = 0;
			foreach (var file in files) {
				var asset = AssetDatabase.LoadAssetAtPath<Spawner>(file);
				if (asset) {
					var result = _register.Register(asset);
					if (result) {
						count++;
					} else {
						Debug.LogWarning($"{asset.Id} is already registred!");
					}
				}
			}
			Debug.Log($"Registred {count} items");
		}
	}
}