﻿using Game.Resources.Containers;
using System;
using System.Linq;
using UnityEngine;

namespace Game.Bees {
	public class Beehive: MonoBehaviour {
		[Min(1)]
		[SerializeField] private int _slotsCount = 2;
		[Min(1)]
		[SerializeField] private int _baseWorktime = 60; // in ticks
		[Space]
		[SerializeField] private Container _container;

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
			slot.SetBee(null);
			return true;
		}
		private void AddResult(BeeBase bee) {
			var count = bee.GetOutputCount();
			if (count > 0) {
				_container.Add(bee.GetOutput(), count);
			}
		}

		private void OnTick() {
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
					if (slot.Timer >= _baseWorktime) {
						var bee = slot.Bee;
						if (Drop(slot)) {
							AddResult(bee);
						}
					}
				}
			}
		}

		[Serializable]
		private class BeeSlot {
			public BeeBase Bee { get; private set; }
			public int Timer { get; private set; } = 0;

			public bool IsFree => Bee == null;

			public BeeSlot() { Timer = 0; }
			public BeeSlot(BeeBase? bee) {
				Bee = bee;
				Timer = 0;
			}

			public void SetBee(BeeBase? bee) {
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