using Game.Bees.AI;
using Game.Entities;
using UnityEngine;

namespace Game.Bees {
	public class Bee: LivingEntity {
		[field: SerializeField] public BeeBase Data { get; private set; }

		private GoFlowerGoal _flowerGoal;
		private GoBeehiveGoal _beehiveGoal;

		protected override void Awake() {
			_flowerGoal = new GoFlowerGoal(this, 0);
			GoalSelector.Add(_flowerGoal);

			_beehiveGoal = new GoBeehiveGoal(this, 1);
			GoalSelector.Add(_beehiveGoal);
		}

		public void SetData(BeeBase genes) {
			Data = genes;
		}

		private void TryEnterBeehive(Beehive beehive) {
			if (beehive.Add(Data)) {
				Destroy(gameObject);
			}
		}

		public override void OnTick() {
			// do some cool stuff
			base.OnTick();
		}
	}
}