using Game.Debugging;
using Game.Entities;
using Game.Serialization;
using Game.Serialization.DataTags;
using System.Text;
using UnityEngine;

namespace Game.Testing {
	[RequireComponent(typeof(DebugObject))]
	public class SerializableEntity: Entity, IDebugInfoProvider {
		private int _timer = 0;
		private byte[] _data;

		private void Update() {
			if (Input.GetKeyDown(KeyCode.S)) {
				var entity = new EntityTag("entity", "test:test");
				this.WriteData(entity);
				_data = entity.Serialize();
				Debug.Log($"Saved! Size: {_data.Length} bytes.");
			}
			if (_data != null && Input.GetKeyDown(KeyCode.L)) {
				var entity = TagDeserializer.Deserialize(_data) as EntityTag;
				this.ReadData(entity);
				Debug.Log($"Loaded! Id: {entity.Id}.");
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

		protected override void WriteAdditionalData(CompoundedTag tag) {
			tag.Add(new IntTag("_timer", _timer));
		}
		protected override void ReadAdditionalData(CompoundedTag tag) {
			_timer = tag.Get<IntTag>("_timer")?.Value ?? 0;
		}
	}
}