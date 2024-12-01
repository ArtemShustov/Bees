using System.Linq;
using Game.Entities.AI;
using Game.Utils;
using UnityEngine;

namespace Game.Bees.AI {
	public class FindBeehiveGoal: Goal {
		private readonly BeeAiBrain _bee;
		private readonly float _dist;

		public FindBeehiveGoal(BeeAiBrain bee, float dist = 10) {
			_bee = bee;
			_dist = dist;
		}

		public override bool CanStart() => !_bee.Home.Get();
		public override bool CanContinueRun() => !_bee.Home.Get();

		public override void Start() {
			var beehive = Physics2D.OverlapCircleAll(_bee.transform.position, _dist)
				.Where(o => o.GetComponent<Beehive>())
				.Select(o => o.GetComponent<Beehive>())
				.GetRandom();
			_bee.SetHome(beehive);
		}
		public override void Stop() { }
		public override void OnTick() { }
	}
}