namespace Game.Entities.AI {
	public abstract class Goal {
		public int Priority { get; private set; }
		public LivingEntity Entity { get; private set; }

		public Goal(LivingEntity entity, int priority) {
			Entity = entity;
			Priority = priority;
		}

		public abstract void Start();
		public abstract void Stop();
		public abstract void OnTick();

		public abstract bool CanStart();
		public abstract bool IsRunning();
	}
}