using UnityEngine;

namespace Game.Entities.AI {
	public class WanderGoal: GoPositionGoal {
		private Transform _self;
		private float _maxDist;

		public WanderGoal(Movement movement, float maxDist, float stopDist = 0.1f): base(movement, stopDist) {
			_self = movement.transform;
			_maxDist = maxDist;
			SetRandomPosition();
		}

		private void SetRandomPosition() {
			var position = (Vector2)_self.position + Random.insideUnitCircle * _maxDist;
			SetTarget(position);
		}
		public override void Start() {
			SetRandomPosition();
			base.Start();
		}
	}
}