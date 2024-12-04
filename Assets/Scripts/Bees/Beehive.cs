using System.Collections.Generic;
using System.IO;
using System.Linq;
using Game.Entities;
using Game.Items;
using Game.Serialization;
using Game.Utils;
using UnityEngine;

namespace Game.Bees {
	public class Beehive: TickableEntity {
		[Min(0), HideInPlayMode]
		[SerializeField] private int _slotsCount = 1;
		[SerializeField] private int _sleepTime = 60;
		[SerializeField] private Storage _storage;
		[SerializeField] private Bee _prefab;
		
		private BeeSlot[] _slots;
		
		public IReadOnlyCollection<BeeSlot> Slots => _slots;
		public Storage Storage => _storage;
		
		private void Awake() {
			_slots = new BeeSlot[_slotsCount];
			for (int i = 0; i < _slotsCount; i++) {
				_slots[i] = new BeeSlot();
			}
		}

		public bool HasFreeSlot() {
			return _slots.Any(slot => slot.IsFree());
		}
		public bool AddBee(Bee bee) {
			var slot = _slots.FirstOrDefault(slot => slot.IsFree());
			if (slot == null) {
				return false;
			}
			slot.Set(bee.Base);
			return true;
		}
		public Entity SpawnBee(BeeBase bee) {
			var entity = Instantiate(_prefab);
			entity.transform.position = transform.position;
			entity.SetBase(bee);
			entity.SetHome(this);
			Level.AddEntityWithNewGUID(entity);
			return entity;
		}
		
		protected override void OnTick() {
			foreach (var slot in _slots) {
				if (!slot.IsFree()) {
					slot.OnTick();
					if (slot.Timer >= _sleepTime) {
						OnSlotTimerEnd(slot);
					}
				}
			}
		}
		private void OnSlotTimerEnd(BeeSlot slot) {
			var bee = slot.Bee;

			if (!bee.CanWork(Level.Time.Current)) {
				return;
			}

			if (bee.HasNektar && bee.Product && bee.Productivity) {
				var output = bee.GetOutput();
				var count = bee.GetOutputCount();
				_storage.TryAdd(output, count);
			}
			bee.HasNektar = false;
			
			SpawnBee(bee);
			slot.Set(null);
		}

		public override void WriteDataTo(DataTag root) {
			base.WriteDataTo(root);

			var slots = _slots.Select(slot => slot.ToTag()).ToArray();
			root.Set("Slots", slots);
		}
		public override void ReadDataFrom(DataTag root) {
			base.ReadDataFrom(root);

			var slots = root.Get<DataTag[]>("Slots", null);
			if (slots != null && slots.Length > 0) {
				_slots = slots.Select(BeeSlot.FromTag).ToArray();
			}
		}

		public override void GetDebugInfo(TextWriter writer) {
			base.GetDebugInfo(writer);
			writer.WriteLine($"Beehive: {_slots.Length}");
			foreach (var slot in _slots) {
				writer.WriteLine($"> Slot: {slot.Timer}");
			}
			writer.WriteLine($"Storage: {_storage.Items.Count}");
			foreach (var item in _storage.Items) {
				writer.WriteLine($"> {item.Count} of {item.Item.Id}");
			}
		}
	}
}