using UnityEngine;

namespace Game.Entities.AI {
	public class SuicideGoal: Goal {
		private Entity _entity;

		public SuicideGoal(Entity entity) {
			_entity = entity;
		}

		public override bool CanStart() => true;
		public override bool CanContinueRun() => false;

		public override void Start() {
			_entity.Destory();
			Debug.Log($"AI Suicide: {_entity.name}({_entity.Guid}) is {_entity.GetType()}");
		}
		public override void Stop() { }
		public override void OnTick() { }
	}
}