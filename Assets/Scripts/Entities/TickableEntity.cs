using Game.World;

namespace Game.Entities {
	public abstract class TickableEntity: Entity, ITickable {
		protected Ticker Ticker { get; private set; }
		private bool _subscribed = false;
		
		protected abstract void OnTick();
		
		public virtual void SetTicker(Ticker ticker) {
			Unsubscribe();
			Ticker = ticker;
			Subscribe();
		}

		private void Subscribe() {
			if (Ticker != null && !_subscribed && enabled) {
				Ticker.Tick += OnTick;
			}
		}
		private void Unsubscribe() {
			if (Ticker != null && _subscribed) {
				Ticker.Tick -= OnTick;
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