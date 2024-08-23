using Game.Entities.AI;
using UnityEngine;

namespace Game.Entities {
	[RequireComponent(typeof(EntityMovement))]
	public class LivingEntity: Entity {
		[field: SerializeField] public EntityMovement Movement { get; private set; }
		[field: SerializeField] public GoalSelector GoalSelector { get; private set; } = new GoalSelector();

		protected virtual void Awake() {
			Movement = GetComponent<EntityMovement>();
		}

		public override void OnTick() {
			GoalSelector.OnTick();
		}
	}
}