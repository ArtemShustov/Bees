using Game.Bees.Genome;
using Game.Entities.Registry;
using Game.Registries;
using Game.Resources;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameEditor {
	public class InternalRegistiesSettings: SettingsProvider {
		private InternalRegistry<Item> _items;
		private InternalRegistry<BeeGen> _genes;
		private InternalRegistry<EntityType> _entities;

		public InternalRegistiesSettings() : base("Registries/Internal", SettingsScope.Project) {
			// 
		}

		[SettingsProvider]
		public static SettingsProvider CreateSettingsProvider() {
			var provider = new InternalRegistiesSettings();
			return provider;
		}
		public override void OnActivate(string searchContext, VisualElement rootElement) {
			_items = new InternalRegistry<Item>(GetAsset<ItemRegistryAsset>(ItemRegistry.Name), "Assets/Data/Items");
			_genes = new InternalRegistry<BeeGen>(GetAsset<GenRegistryAsset>(GenRegistry.Name), "Assets/Data/Genes");
			_entities = new InternalRegistry<EntityType>(GetAsset<EntityRegistryAsset>(EntityRegistry.Name), "Assets/Data/Entities");
		}
		public override void OnGUI(string searchContext) {
			EditorGUILayout.HelpBox("Work in progress", MessageType.Warning);

			DrawInternalRegistry(_items);
			DrawInternalRegistry(_genes);
			DrawInternalRegistry(_entities);
		}

		private void DrawInternalRegistry<T>(InternalRegistry<T> internalRegistry) where T: IRegistryItem {
			DrawAssetList(internalRegistry);
			DrawLoadButton(internalRegistry);
		}
 		private void DrawLoadButton<T>(InternalRegistry<T> internalRegistry) where T : IRegistryItem {
			if (GUILayout.Button($"Update from '{internalRegistry.DataPath}'")) {
				UpdateAsset(internalRegistry.Asset, internalRegistry.DataPath);
			}
		}
		private void DrawAssetList<T>(InternalRegistry<T> internalRegistry) where T: IRegistryItem {
			EditorGUILayout.Separator();

			internalRegistry.Serialized.Update();
			var property = internalRegistry.Serialized.FindProperty("_list");
			EditorGUILayout.LabelField(internalRegistry.Asset.GetType().ToString());
			EditorGUILayout.PropertyField(property);

			internalRegistry.Serialized.ApplyModifiedPropertiesWithoutUndo();
		}

		private T GetAsset<T>(string name) where T : ScriptableObject {
			var fullPath = "Assets/Resources/Registries/" + name + ".asset";

			var instance = AssetDatabase.LoadAssetAtPath<T>(fullPath);
			if (instance == null) {
				instance = ScriptableObject.CreateInstance<T>();
				if (!AssetDatabase.IsValidFolder("Assets/Resources")) {
					AssetDatabase.CreateFolder("Assets", "Resources");
				}
				if (!AssetDatabase.IsValidFolder("Assets/Resources/Registries")) {
					AssetDatabase.CreateFolder("Assets/Resources", "Registries");
				}
				AssetDatabase.CreateAsset(instance, fullPath);
				AssetDatabase.SaveAssets();
			}
			return instance;
		}
		private void UpdateAsset<T>(RegistryAsset<T> registryAsset, string path) where T: IRegistryItem {
			if (!AssetDatabase.IsValidFolder(path)) {
				return;
			}
			if (registryAsset == null) {
				return;
			}
			var files = Directory.GetFiles(path, "*.asset", SearchOption.AllDirectories);
			var list = new List<T>();
			foreach (var file in files) {
				var asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(file);
				if (asset is T item) {
					if (list.Find(i => i.Id.Equals(item.Id)) == null) {
						list.Add(item);
					} else {
						Debug.LogWarning($"Item {item.Id} is already registred!");
					}
				}
			}
			registryAsset.SetList(list.ToArray());
			Debug.Log($"Registred {list.Count} items");
			EditorUtility.SetDirty(registryAsset);
			AssetDatabase.SaveAssets();
		}
	}
	public class InternalRegistry<T> where T: IRegistryItem {
		public RegistryAsset<T>  Asset { get; private set; }
		public SerializedObject Serialized { get ; private set; }
		public string DataPath { get; private set; }

		public InternalRegistry(RegistryAsset<T> asset, string dataPath) {
			Asset = asset;
			Serialized = new SerializedObject(asset);
			DataPath = dataPath;
		}
	}
}