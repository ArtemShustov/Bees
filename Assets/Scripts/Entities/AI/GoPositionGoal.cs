using UnityEngine;

namespace Game.Entities.AI {
	public class GoPositionGoal: Goal {
		private Movement _movement;
		private Vector2 _target;
		private float _stopDist;
		
		private bool _hasTarget;
		private bool _isRunning;
		
		public GoPositionGoal(Movement movement, float stopDist = 0.1f) {
			_movement = movement;
			_stopDist = stopDist;
		}
		public void SetTarget(Vector2 position) {
			_target = position;
			_hasTarget = true;
		}
		public void ClearTarget() {
			_hasTarget = false;
		}

		public bool IsTargetReached() => Vector2.Distance(_target, _movement.transform.position) <= _stopDist;
		
		public override bool CanStart() => _hasTarget && !_isRunning;
		public override bool CanContinueRun() => _hasTarget && _isRunning && !IsTargetReached();
		
		public override void Start() {
			_isRunning = true;
		}
		public override void Stop() {
			_movement.Move(Vector2.zero);
			_isRunning = false;
		}
		public override void OnTick() {
			var direction = _target - (Vector2)_movement.transform.position;
			_movement.Move(direction.normalized);
		}
	}
}