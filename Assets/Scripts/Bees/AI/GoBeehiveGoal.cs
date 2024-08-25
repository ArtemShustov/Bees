using Game.Entities.AI;
using UnityEngine;

namespace Game.Bees.AI {
	public class GoBeehiveGoal: GoObjectGoal {
		private float _dist = 1;
		private Bee _bee;

		public GoBeehiveGoal(Bee entity, int priority, float enterDist = 1) : base(entity, priority) {
			_bee = entity;
			_dist = enterDist;
		}

		private bool IsNearBeehive() {
			return Vector2.Distance(_bee.transform.position, _bee.Home.transform.position) < _dist;
		}

		public override void Start() {
			SetTarget(_bee.Home.gameObject);
			base.Start();
		}
		public override void OnTick() {
			if (IsNearBeehive()) {
				if (!_bee.TryEnterBeehive(_bee.Home)) {
					// beehive is full, find another;
					_bee.ClearHome();
				}
			}
			base.OnTick();
		}

		public override bool CanStart() {
			return (_bee.Home != null) && (_bee.BeehiveCooldown <= 0);
		}
		public override bool CanContinueRun() {
			return _bee.Home != null;
		}
	}
}