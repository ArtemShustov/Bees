using UnityEngine;

namespace Game.Entities.AI {
	public class GoObjectGoal: Goal {
		private GameObject _target;
		private bool _running = false;
		
		public GoObjectGoal(LivingEntity entity, int priority) : base(entity, priority) {
		}

		public void SetTarget(GameObject target) {
			_target = target;
			if (_running) {
				Entity.Movement.SetTarget(_target.transform.position);
			}
		}

		public override void Start() {
			if (_target == null || _running) {
				return;
			}
			_running = true;
			Entity.Movement.NewTarget += OnTargetChanged; ;
			Entity.Movement.TargetReached += OnTargetReached; ;
			Entity.Movement.SetTarget(_target.transform.position);
		}
		public override void Stop() {
			if (_running) {
				Entity.Movement.NewTarget -= OnTargetChanged;
				Entity.Movement.TargetReached -= OnTargetReached;
				Entity.Movement.SetTarget(null);	
				_running = false;
			}
		}
		public override void OnTick() {
			if (_target == null) {
				_running = false;
				Entity.Movement.SetTarget(null);
			}
		}
		public override bool CanStart() => !_running && _target != null;
		public override bool IsRunning() => _running;
		
		protected virtual void OnTargetReached(EntityMovement.Target target) {
			_running = false;
		}
		private void OnTargetChanged(EntityMovement.Target target) {
			_running = false;
		}
	}
}