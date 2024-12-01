using Game.Entities;
using Game.Entities.AI;
using UnityEngine;

namespace Game.Bees.AI {
	public class StayNearHome: GoObjectGoal {
		private readonly BeeAiBrain _bee;
		private readonly float _maxDist;
		
		public StayNearHome(BeeAiBrain bee, Movement movement, float maxDist = 20, float stopDist = 3f): base(movement, stopDist) {
			_bee = bee;
			_maxDist = maxDist;
		}

		private bool IsFarFromHome() {
			var home = _bee.Home.Get();
			if (home == null) {
				return false;
			}
			return Vector2.Distance(_bee.transform.position, home.transform.position) >= _maxDist;
		}

		public override bool CanStart() => _bee.Home.Get() && IsFarFromHome();
		public override void Start() {
			SetTarget(_bee.Home.Get().transform);
			base.Start();
		}
	}
}