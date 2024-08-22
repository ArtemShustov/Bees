using Game.World.Ticking;
using UnityEngine;

namespace Game.World {
	public abstract class Entity: MonoBehaviour, ITickable, IEntity {
		[field: SerializeField] protected Level Level { get; private set; }

		public void SetWorld(Level level) {
			Level?.TickManager.Remove(this);
			Level = level;
			if (gameObject.activeSelf) {
				Level.TickManager.Add(this);
			}
		}

		public virtual void OnTick() {
			// do some cool stuff
		}

		protected virtual void OnEnable() {
			if (Level != null) {
				Level.TickManager.Add(this);
			}
		}
		protected virtual void OnDisable() {
			if (Level != null) {
				Level.TickManager.Remove(this);
			}
		}

#if UNITY_EDITOR
		private void OnValidate() {
			if (Application.isPlaying && Level) {
				Debug.Log("Set world from Inspector");
				SetWorld(Level);
			}
		}
#endif
	}
}