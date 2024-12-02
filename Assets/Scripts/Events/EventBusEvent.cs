namespace Game.Events {
	public delegate void EventBusEvent<T>(T eventData) where T: IGameEvent;
}