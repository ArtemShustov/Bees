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
		[SerializeField] private float _findRadius = 100;
		[Space]
		[SerializeField] private Container _container;

		private BeeSlot[] _slots;

		protected override void Awake() {
			base.Awake();
			_slots = new BeeSlot[_slotsCount];
			for (int i = 0; i < _slots.Length; i++) {
				_slots[i] = new BeeSlot();
			}
		}

		public bool Add(BeeBase bee, bool hasNektar) {
			var slot = _slots.First((slot) => slot.IsFree);
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

		private Flower GetFlower() {
			var flowers = Level.EntitiesList.FindInRadius<Flower>(transform.position, _findRadius);
			if (flowers.Length == 0) {
				return null;
			}
			var flower = flowers[UnityEngine.Random.Range(0, flowers.Length)];
			return flower;
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
						if (slot.HasNektar) {
							AddResult(slot.Bee);
						}
						Drop(slot);
					}
				}
			}
		}

		public void AddDebugInfo(StringBuilder builder) {
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
			public bool HasNektar { get; private set; } = false;

			public bool IsFree => Bee == null;

			public BeeSlot() { Timer = 0; }
			public BeeSlot(BeeBase bee, bool hasNektar) {
				Bee = bee;
				Timer = 0;
				HasNektar = hasNektar;
			}

			public void SetBee(BeeBase bee, bool hasNektar) {
				Bee = bee;
				Timer = 0;
				HasNektar = hasNektar;
			}

			public void OnTick() {
				if (Bee != null) {
					Timer += 1;
				}
			}
		}
	}
}