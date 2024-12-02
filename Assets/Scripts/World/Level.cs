using System;
using System.Collections.Generic;
using Game.Entities;
using UnityEngine;

namespace Game.World {
	public class Level: MonoBehaviour {
		[field: SerializeField] public Ticker Ticker { get; private set; }
		[field: SerializeField] public LevelTime Time { get; private set; }
		
		private List<Entity> _entities = new List<Entity>();
		
		public IReadOnlyList<Entity> Entities => _entities;
		
		public void AddEntity(Entity entity) {
			if (entity == null || _entities.Contains(entity)) {
				throw new ArgumentException();
			}
			entity.transform.SetParent(transform);
			_entities.Add(entity);
			entity.Init(this);
		}
		public void AddEntity(Entity entity, Guid guid) {
			entity.SetGUID(guid);
			AddEntity(entity);
		}
		public void AddEntityWithNewGUID(Entity entity) {
			entity.SetGUID(Guid.NewGuid());
			AddEntity(entity);
		}
		public void RemoveEntity(Entity entity) {
			if (entity == null || !_entities.Contains(entity)) {
				throw new ArgumentException();
			}
			_entities.Remove(entity);
		}
	}
}