using Game.Entities.AI;
using UnityEngine;

namespace Game.Bees.AI {
	public class FindBeehiveGoal: Goal {
		private Bee _bee;
		private float _maxDist = 1;

		public FindBeehiveGoal(Bee entity, int priority, float maxDist = 10) : base(entity, priority) {
			_bee = entity;
			_maxDist = maxDist;
		}

		private Beehive FindBeehive() {
			var hive = _bee.Level.EntitiesList.FindNeareast<Beehive>(_bee.transform.position);
			if (hive != null) {
				var dist = Vector2.Distance(hive.transform.position, _bee.transform.position);
				if (dist > _maxDist) {
					return null;
				}
			}
			return hive;
		}

		public override void Start() {
			var beehive = FindBeehive();
			if (beehive != null) {
				_bee.SetHome(beehive);
			}
		}
		public override void Stop() { }
		public override void OnTick() { }

		public override bool CanContinueRun() {
			return false;
		}
		public override bool CanStart() {
			return _bee.Home == null;
		}
	}
}