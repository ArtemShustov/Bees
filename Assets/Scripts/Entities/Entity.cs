using System;
using System.IO;
using Game.Debugging;
using Game.Serialization;
using Game.World;

namespace Game.Entities {
	public class Entity: SerializableObject, ISerializableComponent, IDebugInfoProvider {
		public Guid Guid { get; private set; }
		public Level Level { get; private set; }

		public virtual void Init(Level level) {
			this.Level = level;
			foreach (var tickable in GetComponents<ITickable>()) {
				tickable.SetTicker(Level.Ticker);
			}
		}
		public void SetGUID(Guid guid) {
			this.Guid = guid;
		}
		public void Destory() {
			Level.RemoveEntity(this);
			Level = null;
			Destroy(gameObject);
		}
		
		public virtual void WriteDataTo(DataTag root) {
			root.Set(nameof(Guid), this.Guid);
		}
		public virtual void ReadDataFrom(DataTag root) {
			this.Guid = root.GetGuid(nameof(Guid), this.Guid);
		}
		public virtual void GetDebugInfo(TextWriter writer) {
			writer.WriteLine($"Entity: {Id}({Guid})");
		}

		public void OnDestroy() {
			if (Level != null) {
				Level.RemoveEntity(this);
			}
		}
	}
}