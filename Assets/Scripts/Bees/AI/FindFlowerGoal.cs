using System.Linq;
using Game.Entities.AI;
using Game.Utils;
using UnityEngine;

namespace Game.Bees.AI {
	public class FindFlowerGoal: Goal {
		private readonly BeeAiBrain _bee;
		private readonly float _dist;

		public FindFlowerGoal(BeeAiBrain bee, float dist = 10) {
			_bee = bee;
			_dist = dist;
		}

		public override bool CanStart() => !_bee.Flower.Get();
		public override bool CanContinueRun() => !_bee.Flower.Get();

		public override void Start() {
			var flower = Physics2D.OverlapCircleAll(_bee.transform.position, _dist)
				.Where(o => o.GetComponent<Flower>())
				.Select(o => o.GetComponent<Flower>())
				.GetRandom();
			_bee.SetFlower(flower);
		}
		public override void Stop() { }
		public override void OnTick() { }
	}
}