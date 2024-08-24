using UnityEngine;

namespace Game.Entities.AI {
	public class GoPositionGoal: Goal {
		private EntityMovement.Target _target;
		private bool _running = false;

		public GoPositionGoal(LivingEntity entity, int priority) : base(entity, priority) {
		}

		public void SetTarget(Vector2 point) {
			_target = new EntityMovement.Target(point);
			if (_running) {
				Entity.Movement.MoveTo(_target);
			}
		}
		private void ForceStop() {
			_running = false;
			Entity.Movement.TargetReached -= OnTargetReached;
		}

		public override void Start() {
			if (_target == null || _running) {
				return;
			}
			_running = true;
			Entity.Movement.MoveTo(_target);
			Entity.Movement.TargetReached += OnTargetReached;
		}
		public override void Stop() {
			if (_running) {
				ForceStop();
			}
		}
		public override void OnTick() {
			if (_running && _target == null) {
				_running = false;
				Entity.Movement.MoveTo(null);
			}
		}
		public override bool CanStart() => !_running && _target != null;
		public override bool CanContinueRun() => _running;

		protected virtual void OnTargetReached(EntityMovement.Target target) {
			ForceStop();
		}
	}
}