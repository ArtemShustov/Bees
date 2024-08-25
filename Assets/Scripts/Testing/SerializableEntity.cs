using Game.Debugging;
using Game.Entities;
using Game.Serialization.DataTags;
using System.Text;
using UnityEngine;

namespace Game.Testing {
	[RequireComponent(typeof(DebugObject))]
	public class SerializableEntity: Entity, IDebugInfoProvider, ITagSerializable<CompoundedTag> {
		private int _timer = 0;
		private byte[] _data;

		private void Update() {
			if (Input.GetKeyDown(KeyCode.S)) {
				var tag = new CompoundedTag("entity");
				WriteData(tag);
				_data = tag.Serialize();
				Debug.Log($"Saved! Size: {_data.Length} byte.");
			}
			if (_data != null && Input.GetKeyDown(KeyCode.L)) {
				var tag = CompoundedTag.Create(_data);
				ReadData(tag);
				Debug.Log($"Loaded!");
			}
		}
		public override void OnTick() {
			base.OnTick();

			_timer++;
		}

		public void AddDebugInfo(StringBuilder builder) {
			builder.AppendLine($"GUID: {GetGUID()}");
			builder.AppendLine($"Timer: {_timer}");
		}

		public void WriteData(CompoundedTag tag) {
			tag.Add(new IntTag("_timer", _timer));
		}
		public void ReadData(CompoundedTag tag) {
			_timer = tag.Get<IntTag>("_timer")?.Value ?? 0;
		}
	}
}