using Game.Entities;
using Game.World.Ticking;
using UnityEngine;

namespace Game.World {
	[RequireComponent(typeof(TickManager))]
	[RequireComponent(typeof(EntitiesList))]
	public class Level: MonoBehaviour {
		[field: SerializeField] public TickManager TickManager { get; private set; }
		[field: SerializeField] public EntitiesList EntitiesList { get; private set; }

		private void Awake() {
			TickManager = GetComponent<TickManager>();
			EntitiesList = GetComponent<EntitiesList>();
		}
		
		public void Add(IEntity entity) { 
			EntitiesList.Track(entity);
			if (entity is ITickable tickable) {
				TickManager.Add(tickable);
			}
		}
		public void Remove(IEntity entity) {
			EntitiesList.Remove(entity);
			if (entity is ITickable tickable) {
				TickManager.Remove(tickable);
			}
		}

		public T Spawn<T>(Vector2 position) where T: Entity {
			var type = GlobalRegistries.Entities.Get<T>();
			if (type == null) {
				Debug.LogWarning($"Entity '{typeof(T)}' not found in registry!");
			}
			return type?.Spawn(this, position) as T;
		}
 	}
}