using Game.Bees.AI;
using Game.Entities;
using UnityEngine;

namespace Game.Bees {
	public class Bee: LivingEntity {
		[field: SerializeField] public BeeBase Data { get; private set; }

		[field: SerializeField] public Beehive Home { get; private set; }
		[field: SerializeField] public Flower Flower { get; private set; }

		protected override void Awake() {
			base.Awake();
			GoalSelector.Add(new CollectNektarGoal(this, 0));
			GoalSelector.Add(new GoBeehiveGoal(this, 1));
		}

		public void SetData(BeeBase beebase, Beehive home, Flower flower) {
			Data = beebase;
			Home = home;
			Flower = flower;
		}
		public void ClearHome() {
			Home = null;
		}

		public bool TryEnterBeehive(Beehive beehive) {
			if (beehive.Add(Data)) {
				Destroy(gameObject);
				return true;
			}
			return false;
		}
	}
}