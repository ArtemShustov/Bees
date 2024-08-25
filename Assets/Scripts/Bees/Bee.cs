using Game.Bees.AI;
using Game.Debugging;
using Game.Entities;
using Game.Entities.AI;
using Game.World;
using System.Text;
using UnityEngine;

namespace Game.Bees {
	public class Bee: LivingEntity, IDebugInfoProvider {
		[field: SerializeField] public BeeBase Data { get; private set; }

		public Beehive Home => _home?.Get();
		public Flower Flower => _flower?.Get();

		public int BeehiveCooldown { get; private set; } = 0; // add data
		public bool HasNektar { get; set; } = false; // add data

		private TrackedEntity<Beehive> _home;
		private TrackedEntity<Flower> _flower;

		protected override void Awake() {
			ResetBeehiveCooldown();
			base.Awake();
			GoalSelector.Add(new CollectNektarGoal(this, 0));
			GoalSelector.Add(new FindBeehiveGoal(this, 1, maxDist: 20));
			GoalSelector.Add(new GoBeehiveGoal(this, 2));
			GoalSelector.Add(new WanderGoal(this, 3, maxDist: 10));

			GoalSelector.Add(new SuicideGoal(this, 10));
		}

		public void SetData(BeeBase beebase, Beehive home, Flower flower) {
			Data = beebase;
			_home = new TrackedEntity<Beehive>(Level, home);
			_flower = new TrackedEntity<Flower>(Level, flower);
		}
		public void ClearHome() {
			_home = null;
		}
		public void SetHome(Beehive beehive) {
			_home = new TrackedEntity<Beehive>(Level, beehive);
		}
		public void ResetBeehiveCooldown() {
			BeehiveCooldown = 600;
		}

		public bool TryEnterBeehive(Beehive beehive) {
			if (beehive.Add(Data, HasNektar)) {
				Destroy(gameObject);
				return true;
			}
			return false;
		}

		public override void OnTick() {
			if (BeehiveCooldown < 0) {
				BeehiveCooldown = 0;
			}
			if (BeehiveCooldown > 0) {
				BeehiveCooldown -= 1;
			}
			base.OnTick();
		}
		public override void AddInfo(StringBuilder builder) {
			base.AddInfo(builder);
			builder.AppendLine($" * WorkCooldown: {BeehiveCooldown}");
			builder.AppendLine($" * Has nektar: {HasNektar}");
		}
	}
}