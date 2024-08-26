using Game.Entities;
using System;

namespace Game.World {
	[Serializable]
	public class TrackedEntity<T> where T: Entity {
		public Guid GUID { get; private set; }
		[UnityEngine.SerializeField] private T _entity;
		private Level _level;

		public TrackedEntity(Level level, T entity) {
			_entity = entity;
			_level = level;
			if (entity != null) {
				GUID = entity.GetGUID();
			}
		}
		public TrackedEntity(Level level, Guid guid) {
			_level = level;
			GUID = guid;
		}

		public T Get() {
			if (_entity == null) {
				if (_level == null || GUID == Guid.Empty) {
					return null;
				}
				if (_level.EntitiesList.GetByGUID(GUID) is T entity) { 
					_entity = entity;
				} else {
					GUID = Guid.Empty;
				}
			}
			return _entity;
		}
	}
}