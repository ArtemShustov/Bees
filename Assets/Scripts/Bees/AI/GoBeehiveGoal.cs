using Game.Entities;
using Game.Entities.AI;
using UnityEngine;

namespace Game.Bees.AI {
	public class GoBeehiveGoal: GoObjectGoal {
		private readonly BeeAiBrain _bee;
		private readonly int _cooldown;

		public GoBeehiveGoal(BeeAiBrain bee, Movement movement, int cooldown, float stopDist = 0.1f): base(movement, stopDist) {
			_bee = bee;
			_cooldown = cooldown;
		}

		private bool IsTimeToSleep() {
			return !_bee.Bee.CanWork();
		}
		
		public override bool CanStart() => _bee.Home.Get() && (_bee.Timer >= _cooldown || IsTimeToSleep());
		public override bool CanContinueRun() => _bee.Home.Get() && base.CanContinueRun();
		public override void Start() {
			SetTarget(_bee.Home.Get().transform);
			base.Start();
		}
		public override void OnTick() {
			base.OnTick();
			if (IsTargetReached()) {
				_bee.Home.Get().AddBee(_bee.Bee);
				GameObject.Destroy(_bee.Bee.gameObject);
			}
		}
	}
}