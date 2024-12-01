using System;
using System.IO;
using Game.Bees.AI;
using Game.Debugging;
using Game.Entities;
using Game.Entities.AI;
using Game.Serialization;
using UnityEngine;

namespace Game.Bees {
	public class BeeAiBrain: AiBrain, ISerializableComponent, IDebugInfoProvider {
		[SerializeField] private int _cooldown = 60;
		[SerializeField] private Bee _bee;
		[SerializeField] private Movement _movement;
		[field: Space]
		[field: SerializeField] public TrackedEntity<Beehive> Home { get; private set; }
		[field: SerializeField] public TrackedEntity<Flower> Flower { get; private set; }

		public int Timer { get; private set; }
		
		public Bee Bee => _bee;

		private void Awake() {
			GoalSelector.Add(new StayNearHome(this, _movement));
			GoalSelector.Add(new FindBeehiveGoal(this));
			GoalSelector.Add(new FindFlowerGoal(this));
			GoalSelector.Add(new CollectNektar(this, _movement, _cooldown / 2));
			GoalSelector.Add(new GoBeehiveGoal(this, _movement, _cooldown));
			GoalSelector.Add(new WanderGoal(_movement, 5));
			GoalSelector.Add(new SuicideGoal(_bee));
		}
		
		public void SetHome(Beehive beehive) {
			Home = new TrackedEntity<Beehive>(beehive);
		}
		public void SetFlower(Flower flower) {
			Flower = new TrackedEntity<Flower>(flower);
		}

		public void ResetTimer() {
			Timer = 0;
		}

		public void WriteDataTo(DataTag root) {
			root.Set(nameof(Home), Home.Guid);
			root.Set(nameof(Flower), Flower.Guid);
			root.Set(nameof(Timer), Timer);
		}
		public void ReadDataFrom(DataTag root) {
			Home.Set(root.GetGuid(nameof(Home), Guid.Empty));
			Flower.Set(root.GetGuid(nameof(Flower), Guid.Empty));
			Timer = (int)root.GetLong(nameof(Timer), 0);
		}

		protected override void OnTick() {
			Timer += 1;
		}

		private void OnValidate() {
			if (Home.Get()) {
				Home = new TrackedEntity<Beehive>(Home.Get());
			}
			if (Flower.Get()) {
				Flower = new TrackedEntity<Flower>(Flower.Get());
			}
		}
		public void GetDebugInfo(TextWriter writer) {
			writer.WriteLine($"BeeAiBrain: {GoalSelector.GetCurrent().GetType()}");
			writer.WriteLine($"> Timer: {Timer}");
			writer.WriteLine($"> Home: {Home.Get()?.Guid}");
			writer.WriteLine($"> Flower: {Flower.Get()?.Guid}");
		}
	}
}