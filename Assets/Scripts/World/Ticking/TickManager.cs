﻿using System.Collections.Generic;
using UnityEngine;

namespace Game.World.Ticking {
	public class TickManager: MonoBehaviour {
		[field: Min(1)]
		[field: SerializeField] public int TickRate { get; private set; } = 1;

		private List<ITickable> _tickables = new List<ITickable>();
		private float _timer = 0;

		public float TickTime => 1f / TickRate;

		private void Update() {
			_timer += Time.deltaTime;
			while (_timer >= TickTime) {
				Tick();
				_timer -= TickTime;
			}
		}

		public void Add(ITickable tickable) {
			_tickables.Add(tickable);
		}
		public void Remove(ITickable tickable) {
			_tickables.Remove(tickable);
		}

		private void Tick() {
			var tickables = new List<ITickable>(_tickables);
			foreach (ITickable tickable in tickables) {
				tickable?.OnTick();
			}
		}
	}
}