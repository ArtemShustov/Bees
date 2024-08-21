using UnityEditor;
using UnityEngine.UIElements;
using UnityEngine;
using System.IO;
using Game.Bees.Genome;

namespace GameEditor.Resources {
	public class GenRegistrySettingsProvider: SettingsProvider {
		private GenRegistry _register;
		private SerializedObject _serialized;

		public GenRegistrySettingsProvider() : base("Registries/Genes", SettingsScope.Project) {
			_register = AssetDatabase.LoadAssetAtPath<GenRegistry>("Assets/Resources/Registries/" + GenRegistry.Name + ".asset");
			if (_register == null) {
				if (AssetDatabase.IsValidFolder("Assets/Resources/Registries") == false) {
					AssetDatabase.CreateFolder("Assets", "Resources");
					AssetDatabase.CreateFolder("Assets/Resources", "Registries");
				}
				_register = ScriptableObject.CreateInstance<GenRegistry>();
				AssetDatabase.CreateAsset(_register, "Assets/Resources/Registries/" + GenRegistry.Name + ".asset");
			}
		}

		[SettingsProvider]
		public static SettingsProvider CreateSettingsProvider() {
			var provider = new GenRegistrySettingsProvider();
			return provider;
		}
		public override void OnActivate(string searchContext, VisualElement rootElement) {
			_serialized = new SerializedObject(_register);
		}
		public override void OnGUI(string searchContext) {
			if (GUILayout.Button("Update from 'Assets/Data/Genes'")) {
				Debug.Log("Updating GenRegistry...");
				LoadFromData();
				_serialized = new SerializedObject(_register);
			}

			EditorGUILayout.PropertyField(_serialized.FindProperty("_list"));
			_serialized.ApplyModifiedPropertiesWithoutUndo();
		}

		private void LoadFromData() {
			if (!AssetDatabase.IsValidFolder("Assets/Data/Genes")) {
				return;
			}
			if (_register == null) {
				return;
			}
			var files = Directory.GetFiles("Assets/Data/Genes", "*.asset", SearchOption.AllDirectories);
			var count = 0;
			foreach (var file in files) {
				var asset = AssetDatabase.LoadAssetAtPath<BeeGen>(file);
				if (asset) {
					var result = _register.Register(asset);
					if (result) {
						count++;
					} else {
						Debug.LogWarning($"{asset.Id} is already registred!");
					}
				}
			}
			Debug.Log($"Registred {count} genes.");
		}
	}
}