using Game.World;
using Game.World.Ticking;
using System;
using UnityEngine;

namespace Game.Entities {
	public abstract class Entity: MonoBehaviour, ITickable, IEntity {
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

		public virtual void OnTick() { }

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