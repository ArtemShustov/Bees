using UnityEngine;

namespace Game.Entities.AI {
	public class GoObjectGoal: GoPositionGoal {
		private Transform _target;
		private bool _isRunning;
		private Vector3 _lastPosition;
		
		public GoObjectGoal(Movement movement, float stopDist = 0.1f): base(movement, stopDist) {
		}
		public void SetTarget(Transform target) {
			_target = target;
			if (target != null) {
				base.SetTarget(target.position);
			}
		}

		public override bool CanStart() => _target && base.CanStart();
		public override bool CanContinueRun() => _target && base.CanContinueRun();

		public override void OnTick() {
			if (_target.position != _lastPosition) {
				_lastPosition = _target.position;
				base.SetTarget(_lastPosition);
			}
			base.OnTick();
		}
	}
}