using System;
using UnityEngine;

namespace Game.World {
	[Obsolete]
	public class InitSceneEntities: MonoBehaviour {
		[SerializeField] private Level _level;

		private void Start() {
			var entities = FindObjectsOfType<Entity>();
			foreach (var entity in entities) {
				entity.SetWorld(_level);
			}
		}
	}
}