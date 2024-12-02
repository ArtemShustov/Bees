namespace Game.Events {
	public static class EventBus<T> where T : IGameEvent {
		private static event EventBusEvent<T> _event;

		public static event EventBusEvent<T> Event {
			add => _event += value;
			remove => _event -= value;
		} 
		
		public static void Raise(T data) {
			_event?.Invoke(data);
		}
		public static void Subscribe(EventBusEvent<T> action) {
			_event += action;
		}
		public static void Unsubscribe(EventBusEvent<T> action) {
			_event -= action;
		}
	}
}