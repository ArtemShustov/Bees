using Game.Entities.AI;
using Game.World;
using UnityEngine;

namespace Game.Entities {
	public class AiBrain: MonoBehaviour, ITickable {
		public GoalSelector GoalSelector { get; private set; } = new GoalSelector();
		protected Ticker Ticker { get; private set; }

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
			if (Ticker != null) {
				Ticker.Tick += OnTick;
				Ticker.AiTick += OnAiTick;
			}
		}
		private void Unsubscribe() {
			if (Ticker != null) {
				Ticker.Tick -= OnTick;
				Ticker.AiTick -= OnAiTick;
			}
		}
		
		private void OnEnable() {
			Subscribe();
		}
		private void OnDisable() {
			Unsubscribe();
		}
	}
}