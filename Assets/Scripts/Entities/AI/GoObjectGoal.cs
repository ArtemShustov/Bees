using UnityEngine;

namespace Game.Entities.AI {
	public class GoObjectGoal: Goal {
		private GameObject _target;
		private Vector2 _position;
		private bool _running = false;

		public GoObjectGoal(LivingEntity entity, int priority) : base(entity, priority) {
		}

		public void SetTarget(GameObject target) {
			_target = target;
			_position = target.transform.position;
			if (_running) {
				Entity.Movement.MoveTo(_position);
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
			Entity.Movement.MoveTo(_position);
			Entity.Movement.TargetReached += OnTargetReached;
		}
		public override void Stop() {
			if (_running) {
				ForceStop();
			}
		}
		public override void OnTick() {
			if (_target == null) {
				_running = false;
				Entity.Movement.MoveTo(null);
			} else {
				var target = (Vector2)_target.transform.position;
				if (target != _position) {
					_position = target;
					Entity.Movement.MoveTo(_position);
				}
			}
		}
		public override bool CanStart() => !_running && _target != null;
		public override bool CanContinueRun() => _running;

		protected virtual void OnTargetReached(EntityMovement.Target target) {
			ForceStop();
		}
	}
}