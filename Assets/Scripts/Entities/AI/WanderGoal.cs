using UnityEngine;

namespace Game.Entities.AI {
	public class WanderGoal: GoPositionGoal {
		private float _maxDist = 1;

		public WanderGoal(LivingEntity entity, int priority, float maxDist) : base(entity, priority) {
			_maxDist = maxDist;
		}

		private void SetRandomPosition() {
			var dest = UnityEngine.Random.insideUnitCircle.normalized * (_maxDist * UnityEngine.Random.Range(0.3f, 1));
			SetTarget((Vector2)Entity.transform.position + dest);
		}

		public override void Start() {
			SetRandomPosition();
			base.Start();
		}
		public override bool CanStart() => true;
	}
}