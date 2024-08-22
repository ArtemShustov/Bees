using Game.World.Ticking;
using UnityEngine;

namespace Game.World {
	[RequireComponent(typeof(TickManager))]
	public class Level: MonoBehaviour {
		[field: SerializeField] public TickManager TickManager { get; private set; }

		private void Awake() {
			TickManager = GetComponent<TickManager>();
		}
	}
}