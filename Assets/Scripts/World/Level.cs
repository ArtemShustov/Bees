using Game.Bees.Genome;
using Game.Entities;
using Game.Entities.Registry;
using Game.Resources;
using Game.Serialization.DataTags;
using Game.World.Ticking;
using System.Collections.Generic;
using UnityEngine;

namespace Game.World {
	[RequireComponent(typeof(TickManager))]
	[RequireComponent(typeof(EntitiesList))]
	public class Level: MonoBehaviour {
		[field: SerializeField] public TickManager TickManager { get; private set; }
		[field: SerializeField] public EntitiesList EntitiesList { get; private set; }

		public GenRegistry GenRegistry => GlobalRegistries.Genes;
		public ItemRegistry ItemRegistry => GlobalRegistries.Items;
		public EntityRegistry EntityRegistry => GlobalRegistries.Entities;

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
		public void Clear() {
			var list = new List<IEntity>(EntitiesList.List);
			foreach (var entity in list) {
				if (entity is MonoBehaviour mono) {
					Destroy(mono.gameObject);
				}
			}
		}

		public T Spawn<T>(Vector2 position) where T: Entity {
			var type = EntityRegistry.Get<T>();
			if (type == null) {
				Debug.LogWarning($"Entity '{typeof(T)}' not found in registry!");
			}
			return type?.Spawn(this, position) as T;
		}
		public IEntity SpawnFromTag(EntityTag tag) {
			var type = EntityRegistry.Get(tag.Id);
			var entity = type.Spawn(this, tag.Position);
			entity.ReadData(this, tag);
			return entity;
		}
	}
}