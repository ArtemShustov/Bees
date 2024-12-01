using Game.Entities;
using UnityEngine;

namespace Game.World {
	public class InitSceneEntities: MonoBehaviour {
		[SerializeField] private Level _level;

		private void Awake() {
			var entities = FindObjectsByType<Entity>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
			foreach (var entity in entities) {
				_level.AddEntityWithNewGUID(entity);
			}
			Destroy(this);
		}
	}
}