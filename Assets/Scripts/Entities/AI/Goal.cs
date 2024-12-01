namespace Game.Entities.AI {
	public abstract class Goal {
		public abstract void Start();
		public abstract void Stop();
		public abstract void OnTick();

		public abstract bool CanStart();
		public abstract bool CanContinueRun();
	}
}