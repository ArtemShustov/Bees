using Game.Debugging;
using Game.Entities;
using Game.Resources.Containers;
using Game.Serialization.DataTags;
using Game.World;
using Game.World.Ticking;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Game.Bees {
	public partial class Beehive: Entity, IDebugInfoProvider, ITickable {
		[Min(1)]
		[SerializeField] private int _slotsCount = 2;
		[Min(1)]
		[SerializeField] private int _baseSleeptime = 60; // in ticks
		[SerializeField] private float _findRadius = 100;
		[Min(1)]
		[SerializeField] private int _containerCapacity = 1;

		private Container _container;
		private BeeSlot[] _slots;

		protected override void Awake() {
			base.Awake();
			_container = new Container(_containerCapacity);
			_slots = new BeeSlot[_slotsCount];
			for (int i = 0; i < _slots.Length; i++) {
				_slots[i] = new BeeSlot(Level);
			}
		}

		public bool Add(BeeBase bee, bool hasNektar) {
			var slot = _slots.FirstOrDefault((slot) => slot.IsFree);
			if (slot == null) {
				return false;
			}
			slot.SetBee(bee, hasNektar);
			return true;
		}
		private bool Drop(BeeSlot slot) {
			if (slot.IsFree) {
				return false;
			}

			var flower = GetFlower();

			var instance = Level.Spawn<Bee>(transform.position);
			if (instance == null) {
				Debug.LogWarning("Can't spawn Bee.");
				slot.SetBee(null, false);
				return false; 
			}
			instance.SetData(slot.Bee, this, flower);
			slot.SetBee(null, false);
			return true;
		}
		private void AddResult(BeeBase bee) {
			var count = bee.GetOutputCount();
			if (count > 0) {
				var item = Level.ItemRegistry.Get(bee.GetOutput());
				if (item != null) {
					_container.Add(item, count);
				} else {
					Debug.LogWarning($"Item '{bee.GetOutput()}' not found in registry");
				}
			}
		}
		private Flower GetFlower() {
			var flowers = Level.EntitiesList.FindInRadius<Flower>(transform.position, _findRadius);
			if (flowers == null || flowers.Length == 0) {
				return null;
			}
			var flower = flowers[UnityEngine.Random.Range(0, flowers.Length)];
			return flower;
		}

		public virtual void OnTick() {
			UpdateSlots();

			void UpdateSlots() {
				if (_slots == null) {
					return;
				}
				foreach (var slot in _slots) {
					if (slot == null) {
						continue;
					}

					slot.OnTick();
					if (slot.Timer >= _baseSleeptime) {
						if (slot.HasNektar) {
							AddResult(slot.Bee);
						}
						Drop(slot);
					}
				}
			}
		}
		protected override void WriteAdditionalData(CompoundedTag tag) {
			base.WriteAdditionalData(tag);

			var slotsTag = new CompoundedTag(nameof(_slots));
			for (int i = 0; i < _slots.Length; i++) {
				var slot = _slots[i];
				var slotTag = new CompoundedTag($"Slot{i}");
				slot.WriteData(slotTag);
				slotsTag.Add(slotTag);
			}
			tag.Add(slotsTag);

			var container = new CompoundedTag(nameof(_container));
			_container.WriteData(container);
			tag.Add(container);
		}
		protected override void ReadAdditionalData(CompoundedTag tag) {
			base.ReadAdditionalData(tag);

			var slotsTag = tag.Get<CompoundedTag>(nameof(_slots));
			if (slotsTag != null) {
				_slots = new BeeSlot[slotsTag.List.Count];
				for (int i = 0; i < _slots.Length; i++) {
					_slots[i] = new BeeSlot(Level);
					_slots[i].ReadData(Level, slotsTag.List[i] as CompoundedTag);
				}
			}

			var container = tag.Get<CompoundedTag>(nameof(_container));
			if (container != null) {
				_container = new Container();
				_container.ReadData(Level, container);
			}
		}

		public void AddDebugInfo(StringBuilder builder) {
			builder.AppendLine($"Beehive. Slots: {_slots.Length}");
			foreach (var slot in _slots) {
				if (slot != null || !slot.IsFree) {
					builder.AppendLine($"[Slot] Bee: {!slot.IsFree} SleepTimer: {slot.Timer}/{_baseSleeptime}"); 
				}
			}
			_container.AddDebugInfo(builder);
		}
	}
}