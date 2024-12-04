using Game.Entities.AI;
using Game.World;
using UnityEngine;

namespace Game.Entities {
	public class AiBrain: MonoBehaviour, ITickable {
		public GoalSelector GoalSelector { get; private set; } = new GoalSelector();
		protected Ticker Ticker { get; private set; }

		private bool _subscribed = false;
		
		protected virtual void OnTick() {}
		protected virtual void OnAiTick() {
			GoalSelector.OnTick();
		}
		
		public virtual void SetTicker(Ticker ticker) {
			Unsubscribe();
			Ticker = ticker;
			Subscribe();
		}
		private void Subscribe() {
			if (Ticker != null && !_subscribed && enabled) {
				Ticker.Tick += OnTick;
				Ticker.AiTick += OnAiTick;
				_subscribed = true;
			}
		}
		private void Unsubscribe() {
			if (Ticker != null && _subscribed) {
				Ticker.Tick -= OnTick;
				Ticker.AiTick -= OnAiTick;
				_subscribed = false;
			}
		}
		
		protected virtual void OnEnable() {
			Subscribe();
		}
		protected virtual void OnDisable() {
			Unsubscribe();
		}
	}
}