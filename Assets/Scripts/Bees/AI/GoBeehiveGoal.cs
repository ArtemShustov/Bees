using Game.Entities.AI;

namespace Game.Bees.AI {
	public class GoBeehiveGoal: GoObjectGoal {
		private Bee _bee;

		public GoBeehiveGoal(Bee entity, int priority) : base(entity, priority) {
			_bee = entity;
		}
	}
}