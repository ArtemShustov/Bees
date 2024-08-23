using Game.Entities;
using Game.Entities.AI;
using UnityEngine;

namespace Game.Bees.AI {
	public class GoFlowerGoal: GoObjectGoal {
		private GameObject _flower;

		public GoFlowerGoal(LivingEntity entity, int priority) : base(entity, priority) {
		}
	}
}