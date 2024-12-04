using System.Collections.Generic;
using Game.Registries;
using Game.World;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.Serialization.Json {
	public class JsonLevelSerialization: MonoBehaviour {
		[SerializeField] private Level _level;

		private string _save;
		private JsonSerializerSettings _settings;

		private void Awake() {
			_settings = new JsonSerializerSettings() {
				TypeNameHandling = TypeNameHandling.All,
				Formatting = Formatting.Indented,
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
			};
			_settings.Converters.Add(new Vector3Converter());
		}

		private void Update() {
			if (Input.GetKeyDown(KeyCode.S)) {
				Save();
			}
			if (Input.GetKeyDown(KeyCode.L)) {
				foreach (var entity in _level.Entities) {
					Destroy(entity.gameObject);
				}
				Load();
			}
		}
		private void Load() {
			var root = JsonConvert.DeserializeObject<DataTag>(_save, _settings);
			foreach (var entity in root.Get<List<DataTag>>("entities")) {
				var id = entity.Get<string>(nameof(SerializableObject.Id), null);
				var entityType = GlobalRegistries.Entities.Get(id);
				if (entityType == null) {
					Debug.LogWarning($"Can't load entity {id} from registry.");
					continue;
				}
				var instance = entityType.SpawnAt(_level.transform);
				instance.ReadFrom(entity);
				_level.AddEntity(instance);
			}
			Debug.Log("Game loaded!");
		}
		private void Save() {
			var root = new DataTag();
			var list = new List<DataTag>();
			foreach (var entity in _level.Entities) {
				var entityData = new DataTag();
				entity.WriteTo(entityData);
				list.Add(entityData);
			}
			root.Set("entities", list);

			var json = JsonConvert.SerializeObject(root, _settings);
			_save = json;
			Debug.Log(json);
			var size = System.Text.Encoding.Default.GetByteCount(json);
			Debug.Log($"Game saved! Size: {size} bytes ({size / 1024} KB)");
		}
	}
}