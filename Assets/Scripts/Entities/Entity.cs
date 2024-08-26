using Game.Serialization.DataTags;
using Game.World;
using System;
using UnityEngine;

namespace Game.Entities {
	public abstract class Entity: MonoBehaviour, IEntity, ITagSerializable<EntityTag> {
		[field: SerializeField] public Level Level { get; private set; }

		private Guid _guid;

		protected virtual void Awake() {
			_guid = Guid.NewGuid();
		}

		public void SetWorld(Level level) {
			Level?.Remove(this);
			Level = level;
			Level.Add(this);
		}
		public Guid GetGUID() => _guid;

		public void WriteData(EntityTag tag) {
			tag.Position = transform.position;
			tag.Guid = _guid;
			WriteAdditionalData(tag.AdditionalData);
		}
		public void ReadData(Level level, EntityTag tag) {
			transform.position = tag.Position;
			_guid = tag.Guid;
			ReadAdditionalData(tag.AdditionalData);
		}

		protected virtual void WriteAdditionalData(CompoundedTag tag) { }
		protected virtual void ReadAdditionalData(CompoundedTag tag) { }

		protected virtual void OnEnable() {
			if (Level != null) {
				Level.Add(this);
			}
		}
		protected virtual void OnDisable() {
			if (Level != null) {
				Level.Remove(this);
			}
		}
		#if UNITY_EDITOR
		private void OnValidate() {
			if (Application.isPlaying && Level) {
				Debug.Log("Set world from Inspector");
				SetWorld(Level);
			}
		}
		#endif
	}
}