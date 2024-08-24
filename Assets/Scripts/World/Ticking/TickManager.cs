using System.Collections.Generic;
using UnityEngine;

namespace Game.World.Ticking {
	public class TickManager: MonoBehaviour {
		[field: Min(1)]
		[field: SerializeField] public int TickRate { get; private set; } = 1;

		private List<ITickable> _tickables = new List<ITickable>();
		private List<ITickable> _toRemove = new List<ITickable>();
		private List<ITickable> _toAdd = new List<ITickable>();
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
			_toAdd.Add(tickable);
		}
		public void Remove(ITickable tickable) {
			_toRemove.Add(tickable);
		}

		private void Tick() {
			// add all
			foreach (ITickable tickable in _toAdd) {
				_tickables.Add(tickable);
			}
			_toAdd.Clear();
			// tick all
			foreach (ITickable tickable in _tickables) {
				tickable?.OnTick();
			}
			// remove
			foreach (ITickable tickable in _toRemove) {
				_tickables.Remove(tickable);
			}
			_toRemove.Clear();
		}
	}
}