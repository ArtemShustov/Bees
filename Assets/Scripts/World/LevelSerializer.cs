using Game.Serialization;
using Game.Serialization.DataTags;
using UnityEngine;

namespace Game.World {
	public class LevelSerializer: MonoBehaviour {
		[SerializeField] private Level _level;

		private byte[] _data;

		public const int Version = 0;

		private const string EntitiesTag = "Entities";
		private const string VersionTag = "Version";

		public void Update() {
			if (Input.GetKey(KeyCode.F1) && Input.GetKeyDown(KeyCode.S)) {
				Save();
				new SaveFile().WriteData(_data);
			}
			if (Input.GetKey(KeyCode.F1) && Input.GetKeyDown(KeyCode.L)) {
				var save = new SaveFile();
				if (save.Exists()) {
					_data = save.ReadData();
					Load();
				} else {
					Debug.Log("No save file.");
				}
			}
		}

		private void Save() {
			var root = new CompoundedTag("level");
			root.Add(new IntTag(VersionTag, Version));
			var entities = new CompoundedTag(EntitiesTag);
			foreach (var entity in _level.EntitiesList.List) {
				if (entity is ITagSerializable<EntityTag> serializable) {
					var entityType = GlobalRegistries.Entities.Get(entity.GetType());
					if (entityType != null) {
						var tag = new EntityTag("Entity", entityType.Id.ToString());
						serializable.WriteData(tag);
						entities.Add(tag);
					} else {
						Debug.LogWarning($"Can't save entity {entity.GetType()}({entity.GetGUID()})");
					}
				}
			}
			root.Add(entities);
			_data = root.Serialize();
			Debug.Log($"Saved {_data.Length} bytes!");
		}
		private void Load() {
			if (_data == null) {
				Debug.LogError("No level data");
				return;
			}

			_level.Clear();
			var root = TagDeserializer.Deserialize(_data) as CompoundedTag;
			var entities = root.Get<CompoundedTag>(EntitiesTag);
			if (entities != null) {
				foreach (var entity in entities.List) {
					if (entity is EntityTag entityTag) {
						var instance = _level.SpawnFromTag(entityTag);
						if (instance == null) {
							Debug.LogWarning($"Can't spawn entity {entityTag.Id}({entityTag.Guid})");
						}
					}
				}
			}
			Debug.Log("Loaded!");
		}
	}
}