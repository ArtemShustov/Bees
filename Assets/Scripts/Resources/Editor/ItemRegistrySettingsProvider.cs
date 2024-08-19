using UnityEditor;
using Game.Resources;
using UnityEngine.UIElements;
using UnityEngine;
using System.IO;

namespace GameEditor.Resources {
	public class ItemRegistrySettingsProvider: SettingsProvider {
		private ItemRegistry _register;
		private SerializedObject _serialized;

		public ItemRegistrySettingsProvider() : base("Registries/Items", SettingsScope.Project) {
			_register = AssetDatabase.LoadAssetAtPath<ItemRegistry>("Assets/Resources/Registries/" + ItemRegistry.Name + ".asset");
			if (_register == null) {
				if (AssetDatabase.IsValidFolder("Assets/Resources/Registries") == false) {
					AssetDatabase.CreateFolder("Assets", "Resources");
					AssetDatabase.CreateFolder("Assets/Resources", "Registries");
				}
				_register = ScriptableObject.CreateInstance<ItemRegistry>();
				AssetDatabase.CreateAsset(_register, "Assets/Resources/Registries/" + ItemRegistry.Name + ".asset");
			}
		}

		[SettingsProvider]
		public static SettingsProvider CreateSettingsProvider() {
			var provider = new ItemRegistrySettingsProvider();
			return provider;
		}
		public override void OnActivate(string searchContext, VisualElement rootElement) {
			_serialized = new SerializedObject(_register);
		}
		public override void OnGUI(string searchContext) {
			if (GUILayout.Button("Update from 'Assets/Data/Items'")) {
				Debug.Log("Updating ItemRegistry...");
				LoadFromData();
				_serialized = new SerializedObject(_register);
			}

			EditorGUILayout.PropertyField(_serialized.FindProperty("_list"));
			_serialized.ApplyModifiedPropertiesWithoutUndo();
		}

		private void LoadFromData() {
			if (!AssetDatabase.IsValidFolder("Assets/Data/Items")) {
				return;
			}
			if (_register == null) {
				return;
			}
			var files = Directory.GetFiles("Assets/Data/Items", "*.asset", SearchOption.AllDirectories);
			var count = 0;
			foreach (var file in files) {
				var asset = AssetDatabase.LoadAssetAtPath<Item>(file);
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