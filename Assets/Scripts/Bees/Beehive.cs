using Game.Debugging;
using Game.Entities;
using Game.Resources.Containers;
using System;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Game.Bees {
	public class Beehive: Entity, IDebugInfoProvider {
		[Min(1)]
		[SerializeField] private int _slotsCount = 2;
		[Min(1)]
		[SerializeField] private int _baseSleeptime = 60; // in ticks
		[Space]
		[SerializeField] private Container _container;
		[SerializeField] private Flower _flower;

		private BeeSlot[] _slots;

		private void Awake() {
			_slots = new BeeSlot[_slotsCount];
			for (int i = 0; i < _slots.Length; i++) {
				_slots[i] = new BeeSlot();
			}
		}

		public bool Add(BeeBase bee) {
			var slot = _slots.First((slot) => slot.IsFree);
			if (slot == null) {
				return false;
			}
			slot.SetBee(bee);
			return true;
		}
		private bool Drop(BeeSlot slot) {
			if (slot.IsFree) {
				return false;
			}

			var instance = GlobalRegistries.Entities.Get<Bee>()?.Spawn(@Level, transform.position) as Bee;
			if (instance == null) {
				Debug.LogWarning("Entity 'Bee' not found in registry");
				slot.SetBee(null);
				return false; 
			}

			slot.Bee.HasNektar = false;
			instance.SetData(slot.Bee, this, _flower);
			slot.SetBee(null);
			return true;
		}
		private void AddResult(BeeBase bee) {
			var count = bee.GetOutputCount();
			if (count > 0) {
				var item = GlobalRegistries.Items.Get(bee.GetOutput());
				if (item != null) {
					_container.Add(item, count);
				} else {
					Debug.LogWarning($"Item '{bee.GetOutput()}' not found in registry");
				}
			}
		}

		public override void OnTick() {
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
						if (slot.Bee.HasNektar) {
							AddResult(slot.Bee);
						}
						Drop(slot);
					}
				}
			}
		}

		public void AddInfo(StringBuilder builder) {
			builder.AppendLine($"Beehive. Slots: {_slots.Length}");
			foreach (var slot in _slots) {
				if (slot != null || !slot.IsFree) {
					builder.AppendLine($"[Slot] SleepTimer: {slot.Timer}/{_baseSleeptime}"); 
				}
			}
		}

		[Serializable]
		private class BeeSlot {
			public BeeBase Bee { get; private set; }
			public int Timer { get; private set; } = 0;

			public bool IsFree => Bee == null;

			public BeeSlot() { Timer = 0; }
			public BeeSlot(BeeBase bee) {
				Bee = bee;
				Timer = 0;
			}

			public void SetBee(BeeBase bee) {
				Bee = bee;
				Timer = 0;
			}

			public void OnTick() {
				if (Bee != null) {
					Timer += 1;
				}
			}
		}
	}
}