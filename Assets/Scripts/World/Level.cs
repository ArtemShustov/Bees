using Game.Entities;
using Game.World.Ticking;
using UnityEngine;

namespace Game.World {
	[RequireComponent(typeof(TickManager))]
	public class Level: MonoBehaviour {
		[field: SerializeField] public TickManager TickManager { get; private set; }

		private void Awake() {
			TickManager = GetComponent<TickManager>();
		}
		
		public void Add(IEntity entity) { 
			if (entity is ITickable tickable) {
				TickManager.Add(tickable);
			}
		}
		public void Remove(IEntity entity) {
			if (entity is ITickable tickable) {
				TickManager.Remove(tickable);
			}
		}
 	}
}