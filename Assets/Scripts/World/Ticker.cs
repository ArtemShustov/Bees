using System;
using UnityEngine;

namespace Game.World {
	public class Ticker: MonoBehaviour {
		[SerializeField] private int _ticksPerSecond = 20;
		private float _time;
		
		public float TickInterval => 1f / _ticksPerSecond;
		
		public event Action Tick;
		public event Action AiTick;

		private void Update() {
			_time += Time.deltaTime;
			while (_time >= TickInterval) {
				TickAll();
				_time -= TickInterval;
			}
		}
		
		public void TickAll() {
			Tick?.Invoke();
			AiTick?.Invoke();
		}
	}
}