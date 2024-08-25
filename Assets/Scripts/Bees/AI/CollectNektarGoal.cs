using Game.Debugging;
using Game.Entities.AI;
using System.Text;
using UnityEngine;

namespace Game.Bees.AI {
	public class CollectNektarGoal: GoObjectGoal, IDebugInfoProvider {
		public int Timer { get; private set; }
		private int _collectingTime = 1;
		private float _collectingDist = 0.5f;

		private Bee _bee;

		public CollectNektarGoal(Bee entity, int priority, int collectingTime = 200) : base(entity, priority) {
			_bee = entity;
			_collectingTime = collectingTime;
		}

		private bool IsNearFlower() {
			return Vector2.Distance(_bee.transform.position, _bee.Flower.transform.position) < _collectingDist;
		}

		public override void Start() {
			SetTarget(_bee.Flower.gameObject);
			base.Start();
		}
		public override void Stop() {
			Timer = 0;
			base.Stop();
		}
		public override void OnTick() {
			if (IsNearFlower()) {
				Timer += 1;
			}
			if (Timer >= _collectingTime) { 
				_bee.HasNektar = true;
				return;
			}
			base.OnTick();
		}

		public override bool CanStart() {
			return (_bee.HasNektar == false) && (_bee.Flower != null);
		}
		public override bool CanContinueRun() {
			return (_bee.HasNektar == false) && (_bee.Flower != null);
		}

		public void AddInfo(StringBuilder builder) {
			builder.AppendLine($" * Collecting timer: {Timer}/{_collectingTime}");
		}
	}
}