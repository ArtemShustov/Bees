using Game.Bees.Genome;
using Game.EntitySpawner;
using Game.Registries;
using Game.Resources;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting.YamlDotNet.Core;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameEditor {
	public class InternalRegistiesSettings: SettingsProvider {
		private ItemRegistryAsset _items;
		private GenRegistryAsset _genes;
		private EntityRegistryAsset _entities;

		public InternalRegistiesSettings() : base("Registries/Internal", SettingsScope.Project) {
			//
		}

		[SettingsProvider]
		public static SettingsProvider CreateSettingsProvider() {
			var provider = new InternalRegistiesSettings();
			return provider;
		}
		public override void OnActivate(string searchContext, VisualElement rootElement) {
			_items = GetAsset<ItemRegistryAsset>(ItemRegistry.Name);
			_genes = GetAsset<GenRegistryAsset>(GenRegistry.Name);
			_entities = GetAsset<EntityRegistryAsset>(EntityRegistry.Name);
		}
		public override void OnGUI(string searchContext) {
			EditorGUILayout.HelpBox("Work in progress", MessageType.Warning);

			DrawAssetList(_items);
			DrawLoadButton(_items, "Assets/Data/Items");
			
			DrawAssetList(_genes);
			DrawLoadButton(_genes, "Assets/Data/Genes");

			DrawAssetList(_entities);
			DrawLoadButton(_entities, "Assets/Data/Entities");
		}

		private void DrawLoadButton<T>(RegistryAsset<T> asset, string path) where T : IRegistryItem {
			if (GUILayout.Button($"Update from '{path}'")) {
				UpdateAsset(asset, path);
			}
		}
		private void DrawAssetList<T>(RegistryAsset<T> asset) where T : IRegistryItem {
			EditorGUILayout.Separator();

			var serializedObject = new SerializedObject(asset);
			var property = serializedObject.FindProperty("_list");
			EditorGUILayout.LabelField(asset.GetType().ToString());
			EditorGUILayout.PropertyField(property);
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
		}
	}
}