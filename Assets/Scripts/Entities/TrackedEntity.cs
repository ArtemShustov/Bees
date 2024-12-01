using System;
using UnityEngine;

namespace Game.Entities {
	[Serializable]
	public class TrackedEntity<T> where T : Entity {
		public Guid Guid { get; private set; }
		[SerializeField] private T _entity;

		public TrackedEntity(T entity) {
			_entity	= entity;
			Guid = _entity.Guid;
		}
		public TrackedEntity(Guid guid) {
			Guid = guid;
		}

		public void Set(Guid guid) {
			Guid = guid;
			_entity = null;
		}
		public T Get() {
			if (_entity != null) {
				return _entity;
			}
			if (Guid == Guid.Empty) {
				return null;
			}
			var entities = GameObject.FindObjectsByType<Entity>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
			foreach (var entity in entities) {
				if (entity.Guid == Guid && entity is T target) {
					_entity = target;
					return _entity;
				}
			}
			return null;
		}
	}
}