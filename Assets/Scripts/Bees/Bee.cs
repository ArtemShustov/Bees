using Game.Bees.AI;
using Game.Debugging;
using Game.Entities;
using Game.Entities.AI;
using Game.Serialization.DataTags;
using Game.World;
using System;
using System.Text;
using UnityEngine;

namespace Game.Bees {
	public class Bee: LivingEntity, IDebugInfoProvider {
		[SerializeField] private int _baseCooldown = 200;

		public BeeBase Data { get; private set; }
		private TrackedEntity<Beehive> _home;
		private TrackedEntity<Flower> _flower;

		public int BeehiveCooldown { get; private set; } = 0;
		public bool HasNektar { get; set; } = false;

		public Beehive Home => _home?.Get();
		public Flower Flower => _flower?.Get();

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
			BeehiveCooldown = _baseCooldown;
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
		
		protected override void WriteAdditionalData(CompoundedTag tag) {
			base.WriteAdditionalData(tag);
			tag.Add(new IntTag(nameof(BeehiveCooldown), BeehiveCooldown));
			tag.Add(new BoolTag(nameof(HasNektar), HasNektar));
			var data = BeeBaseSerializator.ToTag(nameof(Data), Data);
			tag.Add(data);
			tag.Add(new GuidTag(nameof(_home), _home.GUID));
			tag.Add(new GuidTag(nameof(_flower), _flower.GUID));
		}
		protected override void ReadAdditionalData(CompoundedTag tag) {
			base.ReadAdditionalData(tag);
			BeehiveCooldown = tag.Get<IntTag>("BeehiveCooldown")?.Value ?? 0;
			HasNektar = tag.Get<BoolTag>("HasNektar")?.Value ?? false;
			var dataTag = tag.Get<CompoundedTag>("Data");
			Data = dataTag != null ? BeeBaseSerializator.FromTag(Level.GenRegistry, dataTag) : null;
			
			var home = tag.Get<GuidTag>(nameof(_home))?.Value ?? Guid.Empty;
			if (home != Guid.Empty) {
				_home = new TrackedEntity<Beehive>(Level, home);
			}
			var flower = tag.Get<GuidTag>(nameof(_flower))?.Value ?? Guid.Empty;
			if (home != Guid.Empty) {
				_flower = new TrackedEntity<Flower>(Level, flower);
			}
		}

		public override void AddDebugInfo(StringBuilder builder) {
			base.AddDebugInfo(builder);
			builder.AppendLine($" * BeehiveCooldown: {BeehiveCooldown}");
			builder.AppendLine($" * Has nektar: {HasNektar}");
			builder.AppendLine($" * Product: {Data?.ProductGen?.Id}");
			builder.AppendLine($" * Flower: {Flower?.GetGUID()}");
			builder.AppendLine($" * Home: {Home?.GetGUID()}");
		}
	}
}