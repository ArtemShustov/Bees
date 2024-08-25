using UnityEngine;

namespace Game.Entities.AI {
	public class SuicideGoal: Goal {
		public SuicideGoal(LivingEntity entity, int priority) : base(entity, priority) {
		}

		public override void Start() {
			Debug.LogWarning($"Entity {Entity.GetType()}({Entity.GetGUID()}) committed suicide!");
			GameObject.Destroy(Entity.gameObject);
		}
		public override void Stop() { }
		public override void OnTick() { }

		public override bool CanContinueRun() => true;
		public override bool CanStart() => true;
	}
}