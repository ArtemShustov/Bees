using Game.Entities;
using Game.Entities.AI;

namespace Game.Bees.AI {
	public class CollectNektar: GoObjectGoal {
		private readonly BeeAiBrain _bee;
		private readonly int _cooldowm;

		public CollectNektar(BeeAiBrain bee, Movement movement, int cooldown, float stopDist = 1): base(movement, stopDist) {
			_bee = bee;
			_cooldowm = cooldown;
			SetTarget(_bee.Flower.Get()?.transform);
		}

		public override bool CanStart() => _bee.Flower.Get() && _bee.Timer >= _cooldowm && !_bee.Bee.Base.HasNektar;
		public override bool CanContinueRun() => _bee.Flower.Get() && !_bee.Bee.Base.HasNektar && base.CanContinueRun();

		public override void Start() {
			SetTarget(_bee.Flower.Get()?.transform);
			base.Start();
		}
		public override void OnTick() {
			base.OnTick();
			if (IsTargetReached()) {
				_bee.Bee.SetNektar(true);
			}
		}
	}
}