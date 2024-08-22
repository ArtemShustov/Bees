using Game.World;
using UnityEngine;

namespace Game.Bees {
	public class Bee: Entity {
		[field: SerializeField] public BeeBase Data { get; private set; }

		public void SetData(BeeBase genes) {
			Data = genes;
		}

		public override void OnTick() {
			// do some cool stuff
		}
	}
}