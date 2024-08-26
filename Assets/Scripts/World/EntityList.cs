using Game.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.World {
	public class EntitiesList: MonoBehaviour {
		private List<IEntity> _entities = new List<IEntity>();

		public IReadOnlyList<IEntity> List => _entities;

		public void Track(IEntity entity) {
			_entities.Add(entity);
		}
		public void Remove(IEntity entity) {
			_entities.Remove(entity);
		}

		public IEntity GetByGUID(Guid guid) {
			return _entities.FirstOrDefault(entity => entity.GetGUID().Equals(guid));
		}

		public T FindNeareast<T>(Vector2 position) where T: Entity {
			var list = _entities.Where(item => item is T).Select(item => item as T).ToArray();
			if (list.Length == 0) { 
				return default;
			}
			float neareastDist = float.MaxValue;
			T neareastEntity = null;
			foreach (var entity in list) {
				var dist = Vector2.Distance(position, entity.transform.position);
				if (dist < neareastDist) {
					neareastDist = dist;
					neareastEntity = entity;
				}
			}
			return neareastEntity;
		}
		public T[] FindInRadius<T>(Vector2 position, float radius) where T: Entity {
			var list = _entities.Where(item => item is T).Select(item => item as T).ToArray();
			if (list.Length == 0) {
				return null;
			}

			List<T> result = new List<T>();
			foreach (var entity in list) {
				var dist = Vector2.Distance(position, entity.transform.position);
				if (dist <= radius) { 
					result.Add(entity);
				}
			}
			return result.ToArray();
		}
	}
}